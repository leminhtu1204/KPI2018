using LunchManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LunchManagement.Services
{
    public interface IGenericService <T> where T : BaseClass
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        Task Delete(T entity);
        Task Edit(T entity);
        Task Save();
        Task<T> GetById(int id);
    }
}
