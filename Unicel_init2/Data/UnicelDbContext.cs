using Microsoft.EntityFrameworkCore;
using Unicel_init2.Models.Domain;

namespace Unicel_init2.Data
{
    public class UnicelDbContext: DbContext
    {
        public UnicelDbContext(DbContextOptions<UnicelDbContext> options) : base(options) 
        { 
            
        }

        public DbSet<Filters> Filters { get; set; }

        public DbSet<OEM> OEM { get; set; }
    }
}
