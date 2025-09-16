using System.Collections.Generic;
using SimpleWebApp.Models;
using SimpleWebApp.Repositories;

namespace SimpleWebApp.Services
{
    public class MenuItemService : ServiceBase<MenuItem>, IMenuItemService
    {
        private readonly IMenuItemRepository _repo;

        public MenuItemService() : this(new MenuItemRepository()) { }

        public MenuItemService(IMenuItemRepository repo)
        {
            _repo = repo;
        }

        public override MenuItem GetById(int id) => _repo.GetById(id);
        public override IEnumerable<MenuItem> GetAll() => _repo.GetAll();
        public override void Create(MenuItem entity) => _repo.Insert(entity);
        public override void Update(MenuItem entity) => _repo.Update(entity);
        public override void Delete(int id) => _repo.Delete(id);

        public IEnumerable<MenuItem> GetByCategory(string category) => _repo.GetByCategory(category);
    }
}
