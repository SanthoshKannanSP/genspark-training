using assignment_1.Models;
using Microsoft.EntityFrameworkCore;
namespace assignment_1.Contexts
{
    public class ClinicContext : DbContext
    {
        public ClinicContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
    }
}