// IMenuItemRepository.cs
using System.Collections.Generic;

namespace SimpleWebApp.Repositories
{
    public interface IMenuItemRepository : IRepository<SimpleWebApp.Models.MenuItem>
    {
        IEnumerable<SimpleWebApp.Models.MenuItem> GetByCategory(string category);
    }
}
