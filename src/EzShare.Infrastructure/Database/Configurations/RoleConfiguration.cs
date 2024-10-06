using EzShare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzShare.Infrastructure.Database.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name).IsRequired().HasMaxLength(50);
        var roles = new List<Role>
        {
            new Role { Id = 1, Name = "Admin", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow},
            new Role { Id = 2, Name = "User", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow},
        };
        builder.HasData(roles);
    }
}