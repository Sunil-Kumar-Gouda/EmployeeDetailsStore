using EmployeeDetailStore.Model.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDetailStore.Repository
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeId);

            modelBuilder.Entity<Employee>(b => {
                b.HasIndex(e => new { e.FirstName, e.LastName, e.Email })
                .IsUnique(true);
            });
        }
    }
}
