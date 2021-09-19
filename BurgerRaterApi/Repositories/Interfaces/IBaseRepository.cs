using BurgerRaterApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerRaterApi.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public Task<IEnumerable<T>> GetAll();

        public Task<T> GetById(Guid id);

        public Task<T> Create(T entity);

        public Task<T> Update(T entity);

        public Task Delete(T entity);

        public Task Delete(Guid id);

        public Task<bool> Exists(Guid id);
    }
}
