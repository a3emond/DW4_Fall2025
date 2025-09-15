using System.Web;
using Newtonsoft.Json;
using SimpleWebApp.Services;
using SimpleWebApp.Models;

namespace SimpleWebApp.Controllers
{
    public class AuthController : IController
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController()
        {
            _userService = new UserService();
            _authService = new AuthService();
        }

        public void Handle(HttpContext context)
        {
            var path = context.Request.Path.ToLower();

            if (path == "/api/login")
                Login(context);
            else if (path == "/api/logout")
                Logout(context);
            else
                NotFound(context);
        }

        private void Login(HttpContext context)
        {
            var email = context.Request["email"];
            var password = context.Request["password"];

            var result = _userService.Login(email, password);

            if (result.Success && !string.IsNullOrEmpty(result.Token))
            {
                var cookie = new HttpCookie("auth_token", result.Token)
                {
                    HttpOnly = true,
                    Secure = false, // set to true when using HTTPS
                    Expires = System.DateTime.UtcNow.AddHours(1)
                };
                context.Response.Cookies.Add(cookie);
            }

            JsonResponse(context, result);
        }

        private void Logout(HttpContext context)
        {
            if (context.Request.Cookies["auth_token"] != null)
            {
                var expired = new HttpCookie("auth_token")
                {
                    Expires = System.DateTime.UtcNow.AddDays(-1)
                };
                context.Response.Cookies.Add(expired);
            }

            var result = new ServiceResult
            {
                Success = true,
                Message = "Logged out"
            };

            JsonResponse(context, result);
        }

        private void NotFound(HttpContext context)
        {
            context.Response.StatusCode = 404;

            var result = new ServiceResult
            {
                Success = false,
                Error = "Unknown API route"
            };

            JsonResponse(context, result);
        }

        private void JsonResponse(HttpContext context, object data)
        {
            context.Response.ContentType = "application/json";
            context.Response.Write(JsonConvert.SerializeObject(data));
            context.Response.End();
        }
    }
}
