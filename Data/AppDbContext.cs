using Microsoft.EntityFrameworkCore;
using PowerScaling.Entities;

namespace PowerScaling.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Personagens> Personagens => Set<Personagens>();
    }
}
