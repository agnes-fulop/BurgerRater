using BurgerRaterApi.Data;
using BurgerRaterApi.Infrastructure.Exceptions;
using BurgerRaterApi.Models;
using BurgerRaterApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerRaterApi.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public async Task<T> Create(T entity)
        {
            var createdEntity = _entities.Add(entity);
            await _context.SaveChangesAsync();

            return createdEntity.Entity;
        }

        public async Task Delete(T entity)
        {
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);

            await Delete(entity);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _entities.AnyAsync(entity => entity.Id == id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            var entity = await _entities.FindAsync(id);

            if (entity is null)
            {
                Log.Error($"{typeof(T)} entity with Id {id} can't be found");
                throw new NotFoundException("Entity doesn't exist.");
            }

            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
