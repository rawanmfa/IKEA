﻿using IKEA.DAL.Presistance.Repositories.Departments;
using IKEA.DAL.Presistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        public IEmployeeRepositories EmployeeRepository { get;}
        public IDepartmentRepository DepartmentRepository { get;}
        int Complete();
    }
}
