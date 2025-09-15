using System.Collections.Generic;

namespace SimpleWebApp.Services
{
    // Generic CRUD contract (low-level, raw access)
    public interface IService<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}