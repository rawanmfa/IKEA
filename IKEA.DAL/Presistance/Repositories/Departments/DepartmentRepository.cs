using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _DbContext;
        public DepartmentRepository(ApplicationDbContext dbContext) { _DbContext = dbContext; }
        public IEnumerable<Department> GetAll(bool WithNoTracking = true)
        {
            if (WithNoTracking)
            {
                return _DbContext.Departments.AsNoTracking().ToList();
            }
            return _DbContext.Departments.ToList();
        }
        public Department? GetById(int id)
        {
            return _DbContext.Departments.Find(id);
        }
        public int Add(Department entity)
        {
            _DbContext.Departments.Add(entity);
            return _DbContext.SaveChanges();
        }
        public int Update(Department entity)
        {
            _DbContext.Departments.Update(entity);
            return _DbContext.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _DbContext.Departments.Remove(entity);
            return _DbContext.SaveChanges();
        }
    }
}
