using System.Collections.Generic;
using System.Security.Claims;
using SimpleWebApp.Models;

namespace SimpleWebApp.Services
{
    public interface IUserService : IService<User>
    {
        // Authentication
        ServiceResult Login(string email, string password);
        ServiceResult Register(string username, string email, string password);

        // Role-aware operations
        IEnumerable<User> GetAllUsers(ClaimsPrincipal currentUser);
        ServiceResult DeleteUser(int id, ClaimsPrincipal currentUser);
        ServiceResult UpdateUser(User updated, ClaimsPrincipal currentUser);
    }
}