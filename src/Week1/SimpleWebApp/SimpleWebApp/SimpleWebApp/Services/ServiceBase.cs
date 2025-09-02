using System.Collections.Generic;

namespace SimpleWebApp.Services
{
    public abstract class ServiceBase<T> : IService<T>
    {
        public abstract T GetById(int id);
        public abstract IEnumerable<T> GetAll();
        public abstract void Create(T entity);
        public abstract void Update(T entity);
        public abstract void Delete(int id);
    }
}
