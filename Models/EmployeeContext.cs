using Microsoft.EntityFrameworkCore;
using Sistema_de_Empregados.Models;

namespace Web_Empregados.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }

    }
}