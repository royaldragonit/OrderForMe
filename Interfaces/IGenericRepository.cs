using Microsoft.EntityFrameworkCore;
using OrderForMeProject.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrderForMeProject.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        ResultCustomModel<T> GetById(int id);
        ResultCustomModel<IEnumerable<T>> GetAll();
        ResultCustomModel<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        ResultCustomModel<DbSet<T>> GetQueryAll();
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
