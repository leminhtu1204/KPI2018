using LunchManagement.Models;
using LunchManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LunchManagement.Services
{
    public class GenericService<T> : IGenericService<T> where T : BaseClass
    {
        private readonly IGenericRepository<T> genericRepository;

        public GenericService(IGenericRepository<T> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public async Task Add(T entity)
        {
            await this.genericRepository.Add(entity);
        }

        public async Task Delete(T entity)
        {
            await this.genericRepository.Delete(entity);
        }

        public async Task Edit(T entity)
        {
            await this.genericRepository.Edit(entity);
        }

        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            return await genericRepository.FindBy(predicate);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await genericRepository.GetAll();
        }

        public async Task<T> GetById(int id)
        {
            return await genericRepository.GetById(id);
        }

        public async Task Save()
        {
            await genericRepository.Save();
        }
    }
}
