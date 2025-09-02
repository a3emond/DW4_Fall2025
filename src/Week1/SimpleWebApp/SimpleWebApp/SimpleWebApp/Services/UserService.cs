using System.Collections.Generic;
using System.Security.Claims;
using SimpleWebApp.Common;
using SimpleWebApp.Models;
using SimpleWebApp.Repositories;

namespace SimpleWebApp.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IAuthService _auth;

        public UserService() : this(new UserRepository(), new AuthService()) { }

        public UserService(IUserRepository repo, IAuthService auth)
        {
            _repo = repo;
            _auth = auth;
        }

        // --- Base CRUD (raw, no auth, no wrapping) ---
        public override User GetById(int id) => _repo.GetById(id);
        public override IEnumerable<User> GetAll() => _repo.GetAll();
        public override void Create(User entity) => _repo.Insert(entity);
        public override void Update(User entity) => _repo.Update(entity);
        public override void Delete(int id) => _repo.Delete(id);

        // --- Authentication ---
        public ServiceResult Login(string email, string password)
        {
            var user = _repo.GetByEmail(email);

            if (user == null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Error = "User not found"
                };
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return new ServiceResult
                {
                    Success = false,
                    Error = "Invalid password"
                };
            }

            var token = _auth.GenerateJwt(user);
            return new ServiceResult
            {
                Success = true,
                Message = "Login successful",
                Token = token
            };
        }

        public ServiceResult Register(string username, string email, string password)
        {
            var existing = _repo.GetByEmail(email);
            if (existing != null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Error = "Email already exists"
                };
            }

            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            var newUser = new User
            {
                Username = username,
                Email = email,
                PasswordHash = hash,
                Role = "user"
            };

            _repo.Insert(newUser);

            return new ServiceResult
            {
                Success = true,
                Message = "User registered successfully"
            };
        }

        // --- Role-aware wrappers ---
        public IEnumerable<User> GetAllUsers(ClaimsPrincipal currentUser)
        {
            EnsureAdmin(currentUser);
            return _repo.GetAll();
        }

        public ServiceResult DeleteUser(int id, ClaimsPrincipal currentUser)
        {
            EnsureAdmin(currentUser);

            _repo.Delete(id);
            return new ServiceResult
            {
                Success = true,
                Message = "User deleted successfully"
            };
        }

        public ServiceResult UpdateUser(User updated, ClaimsPrincipal currentUser)
        {
            var role = _auth.GetRole(currentUser);
            var currentUserId = _auth.GetUserId(currentUser);

            if (role != "admin" && updated.Id != currentUserId)
            {
                throw new ForbiddenException("You can only update your own account");
            }

            _repo.Update(updated);

            return new ServiceResult
            {
                Success = true,
                Message = "User updated successfully"
            };
        }

        // --- Helpers ---
        private void EnsureAdmin(ClaimsPrincipal principal)
        {
            var role = _auth.GetRole(principal);
            if (role != "admin")
                throw new ForbiddenException("Admins only");
        }
    }
}
