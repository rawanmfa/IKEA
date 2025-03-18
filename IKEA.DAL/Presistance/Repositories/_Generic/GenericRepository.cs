using IKEA.DAL.Models;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories._Generic
{
    public class GenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _DbContext;
        public GenericRepository(ApplicationDbContext dbContext) { _DbContext = dbContext; }
        public IEnumerable<T> GetAll(bool WithNoTracking = true)
        {
            if (WithNoTracking)
            {
                return _DbContext.Set<T>().Where(x=>!x.IsDeleted).AsNoTracking().ToList();
            }
            return _DbContext.Set<T>().Where(x => !x.IsDeleted).ToList();
        }
        public IQueryable<T> GetAllAsQuerable()
        {
            return _DbContext.Set<T>();
        }
        public T? GetById(int id)
        {
            return _DbContext.Set<T>().Find(id);
        }
        public void Add(T entity)
        {
            _DbContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _DbContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _DbContext.Set<T>().Update(entity);
        }

    }
}
