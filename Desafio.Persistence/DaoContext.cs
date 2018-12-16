using Desafio.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Persistence
{
    public class DaoContext : DbContext
    {
        public DaoContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserPhoneConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
