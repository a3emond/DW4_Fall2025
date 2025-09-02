using System.Web;
using Newtonsoft.Json;
using SimpleWebApp.Services;
using SimpleWebApp.Common; // ForbiddenException
using SimpleWebApp.Models;

namespace SimpleWebApp.Controllers
{
    public class UserController : IController
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserController()
        {
            _userService = new UserService();
            _authService = new AuthService();
        }

        public void Handle(HttpContext context)
        {
            var path = context.Request.Path.ToLower();

            if (path == "/api/register")
                Register(context);
            else if (path == "/api/profile")
                Profile(context);
            else if (path == "/api/admin/users")
                GetAllUsers(context);
            else if (path == "/api/admin/delete")
                DeleteUser(context);
            else if (path == "/api/user/update")
                UpdateUser(context);
            else
                NotFound(context);
        }

        private void Register(HttpContext context)
        {
            var username = context.Request["username"];
            var email = context.Request["email"];
            var password = context.Request["password"];

            var result = _userService.Register(username, email, password);
            JsonResponse(context, result);
        }

        private void Profile(HttpContext context)
        {
            var principal = ValidateRequest(context);
            if (principal == null) return;

            var userId = principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var username = principal.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            var role = principal.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            var result = new ServiceResult
            {
                Success = true,
                Message = "Profile retrieved",
                // Attach user payload
                Token = null, // not needed, but ServiceResult allows this property
            };

            JsonResponse(context, new
            {
                success = result.Success,
                message = result.Message,
                user = new { id = userId, name = username, role = role }
            });
        }

        private void GetAllUsers(HttpContext context)
        {
            var principal = ValidateRequest(context);
            if (principal == null) return;

            try
            {
                var users = _userService.GetAllUsers(principal);
                JsonResponse(context, new { success = true, users });
            }
            catch (ForbiddenException ex)
            {
                context.Response.StatusCode = 403;
                JsonResponse(context, new ServiceResult
                {
                    Success = false,
                    Error = ex.Message
                });
            }
        }

        private void DeleteUser(HttpContext context)
        {
            var principal = ValidateRequest(context);
            if (principal == null) return;

            if (!int.TryParse(context.Request["id"], out var id))
            {
                context.Response.StatusCode = 400;
                JsonResponse(context, new ServiceResult
                {
                    Success = false,
                    Error = "Invalid user id"
                });
                return;
            }

            try
            {
                var result = _userService.DeleteUser(id, principal);
                JsonResponse(context, result);
            }
            catch (ForbiddenException ex)
            {
                context.Response.StatusCode = 403;
                JsonResponse(context, new ServiceResult
                {
                    Success = false,
                    Error = ex.Message
                });
            }
        }

        private void UpdateUser(HttpContext context)
        {
            var principal = ValidateRequest(context);
            if (principal == null) return;

            if (!int.TryParse(context.Request["id"], out var id))
            {
                context.Response.StatusCode = 400;
                JsonResponse(context, new ServiceResult
                {
                    Success = false,
                    Error = "Invalid user id"
                });
                return;
            }

            var updated = new User
            {
                Id = id,
                Username = context.Request["username"],
                Email = context.Request["email"],
                PasswordHash = context.Request["password"],
                Role = context.Request["role"] // service enforces role restrictions
            };

            try
            {
                var result = _userService.UpdateUser(updated, principal);
                JsonResponse(context, result);
            }
            catch (ForbiddenException ex)
            {
                context.Response.StatusCode = 403;
                JsonResponse(context, new ServiceResult
                {
                    Success = false,
                    Error = ex.Message
                });
            }
        }

        // --- Helpers ---
        private System.Security.Claims.ClaimsPrincipal ValidateRequest(HttpContext context)
        {
            var token = context.Request.Cookies["auth_token"]?.Value;
            var principal = _authService.ValidateJwt(token);

            if (principal == null)
            {
                context.Response.StatusCode = 401;
                JsonResponse(context, new ServiceResult
                {
                    Success = false,
                    Error = "Unauthorized"
                });
            }

            return principal;
        }

        private void NotFound(HttpContext context)
        {
            context.Response.StatusCode = 404;
            JsonResponse(context, new ServiceResult
            {
                Success = false,
                Error = "Unknown API route"
            });
        }

        private void JsonResponse(HttpContext context, object data)
        {
            context.Response.ContentType = "application/json";
            context.Response.Write(JsonConvert.SerializeObject(data));
            context.Response.End();
        }
    }
}
