using Microsoft.EntityFrameworkCore;
using OrderForMeProject.Interfaces;
using OrderForMeProject.Models.CustomModels;
using OrderForMeProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrderForMeProject.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly OrderformeContext _db;
        public GenericRepository(OrderformeContext db)
        {
            _db = db;
        }

        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _db.Set<T>().AddRange(entities);
        }

        public ResultCustomModel<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            var data = _db.Set<T>().Where(expression);
            return new ResultCustomModel<IEnumerable<T>>
            {
                Success = true,
                Data = data
            };
        }

        public ResultCustomModel<IEnumerable<T>> GetAll()
        {
            List<T> data = _db.Set<T>().ToList();
            return new ResultCustomModel<IEnumerable<T>>
            {
                Data = data,
                Success = true
            };
        }
        public ResultCustomModel<DbSet<T>> GetQueryAll()
        {
            DbSet<T> data = _db.Set<T>();
            return new ResultCustomModel<DbSet<T>>
            {
                Data = data,
                Success = true
            };
        }

        public ResultCustomModel<T> GetById(int id)
        {
            T data = _db.Set<T>().Find(id);
            return new ResultCustomModel<T>
            {
                Data = data,
                Success = true
            };
        }

        public void Remove(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _db.Set<T>().RemoveRange(entities);
        }
    }
}
