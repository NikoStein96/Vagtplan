using Microsoft.EntityFrameworkCore;
using Vagtplan.Models;

namespace Vagtplan.Data
{
    public class ShiftPlannerContext: DbContext
    {
        public ShiftPlannerContext(DbContextOptions<ShiftPlannerContext> options) : base(options) { }

        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Organisation> Organisations { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Day> Days { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organisation>()
                .HasMany(o => o.Employees)
                .WithOne(e => e.Organisation)
                .HasForeignKey(e => e.OrganisationId);
        }

    }
}
