using SimpleWebApp.Models;

namespace SimpleWebApp.Repositories
{
    public interface IUserRepository :IRepository<User>
    {
        User GetByEmail(string email);
        User GetByUsername(string username);
    }
}