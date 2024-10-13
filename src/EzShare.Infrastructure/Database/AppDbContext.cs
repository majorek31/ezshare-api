using EzShare.Domain.Common;
using EzShare.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EzShare.Infrastructure.Database
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Upload> Uploads { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(AppDbContext).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}