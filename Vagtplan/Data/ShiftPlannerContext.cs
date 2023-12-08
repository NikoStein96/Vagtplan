using Microsoft.EntityFrameworkCore;
using Vagtplan.Models;

namespace Vagtplan.Data
{
    public class ShiftPlannerContext: DbContext
    {
        public ShiftPlannerContext(DbContextOptions<ShiftPlannerContext> options) : base(options) { }
        public DbSet<PreferedWorkDay> PreferedWorkDays { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Day> Days { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the composite key
            modelBuilder.Entity<PreferedWorkDay>()
                .HasKey(ew => new { ew.EmployeeId, ew.Weekday });

            // Configuring the many-to-many relationship
            modelBuilder.Entity<PreferedWorkDay>()
                .HasOne(ew => ew.Employee)
                .WithMany(e => e.PreferedWorkDays)
                .HasForeignKey(ew => ew.EmployeeId);
        }

    }
}
