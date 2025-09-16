using System.Collections.Generic;

namespace SimpleWebApp.Repositories
{
    public interface IMenuItemMediaRepository : IRepository<SimpleWebApp.Models.MenuItemMedia>
    {
        IEnumerable<SimpleWebApp.Models.MenuItemMedia> GetByMenuItemId(int menuItemId);
    }
}
