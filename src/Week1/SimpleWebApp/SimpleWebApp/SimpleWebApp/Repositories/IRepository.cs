using System.Collections.Generic;

namespace SimpleWebApp.Repositories
{
    public interface IRepository<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        
    }
}