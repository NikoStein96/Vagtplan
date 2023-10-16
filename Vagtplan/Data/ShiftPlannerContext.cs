﻿using Microsoft.EntityFrameworkCore;
using Vagtplan.Models;

namespace Vagtplan.Data
{
    public class ShiftPlannerContext: DbContext
    {
        public ShiftPlannerContext(DbContextOptions<ShiftPlannerContext> options) : base(options) { }

        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Employee> Employees { get; set; }








    }
}