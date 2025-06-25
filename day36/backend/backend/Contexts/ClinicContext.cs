using Microsoft.EntityFrameworkCore;

namespace backend.Contexts
{
    public class ClinicContext : DbContext
    {

        public ClinicContext(DbContextOptions options) : base(options)
        {

        }

    }
}