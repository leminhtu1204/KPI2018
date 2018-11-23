using LunchManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LunchManagement.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseClass
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task Add(T entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            var ent = this._dbContext.Set<T>().Find(entity.Id);

            if (ent != null)
            {
                this._dbContext.Set<T>().Remove(entity);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task Edit(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }
    }
}
