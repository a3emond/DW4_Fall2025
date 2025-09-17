using System.Collections.Generic;
using SimpleWebApp.Models;

namespace SimpleWebApp.Services
{
    public interface IMenuItemService : IService<MenuItem>
    {
        IEnumerable<MenuItem> GetByCategory(string category);
    }
}
