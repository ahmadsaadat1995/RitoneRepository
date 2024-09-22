using Microsoft.EntityFrameworkCore;

namespace Ritone.EndPoint
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<SampleEntity> RitoneEntity { get; set; }
    }
}
