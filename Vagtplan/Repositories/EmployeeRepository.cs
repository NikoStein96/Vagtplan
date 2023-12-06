﻿using Google;
using Microsoft.EntityFrameworkCore;
using Vagtplan.Data;
using Vagtplan.Interfaces.Repositories;
using Vagtplan.Models;

namespace Vagtplan.Repositories
{
    public class EmployeeRepository: GenericRepository<Employee>, IEmployeeRepository
    {


        public EmployeeRepository(ShiftPlannerContext context) : base(context)
        {}

        public Employee GetEmployeeWithOrganisation(string firebaseId)
        {
            return _context.Employees
                .Include(e => e.Organisation)
                .FirstOrDefault(e => e.FirebaseId == firebaseId);
        }
    }
}
