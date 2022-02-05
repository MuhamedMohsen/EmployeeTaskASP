using Microsoft.EntityFrameworkCore;
using employee.Models;
namespace employee.Models
{
    public class HRDatabaseContext : DbContext
    {
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=(localdb)\MSSQLLocalDB; Initial Catalog = EmployeeDB; Integrated Security = True;");
        }
    }
}
