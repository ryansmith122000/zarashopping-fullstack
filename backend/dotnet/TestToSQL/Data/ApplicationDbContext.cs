using Microsoft.EntityFrameworkCore;
using TestToSQL.Models;

namespace TestToSQL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Users> Users { get; set; }
    }
}
