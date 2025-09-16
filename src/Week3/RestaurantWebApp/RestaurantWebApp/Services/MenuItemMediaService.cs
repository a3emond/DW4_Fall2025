using System.Collections.Generic;
using SimpleWebApp.Models;
using SimpleWebApp.Repositories;

namespace SimpleWebApp.Services
{
    public class MenuItemMediaService : ServiceBase<MenuItemMedia>, IMenuItemMediaService
    {
        private readonly IMenuItemMediaRepository _repo;

        public MenuItemMediaService() : this(new MenuItemMediaRepository()) { }

        public MenuItemMediaService(IMenuItemMediaRepository repo)
        {
            _repo = repo;
        }

        public override MenuItemMedia GetById(int id) => _repo.GetById(id);
        public override IEnumerable<MenuItemMedia> GetAll() => _repo.GetAll();
        public override void Create(MenuItemMedia entity) => _repo.Insert(entity);
        public override void Update(MenuItemMedia entity) => _repo.Update(entity);
        public override void Delete(int id) => _repo.Delete(id);
    }
}
