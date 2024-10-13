using EzShare.Domain.Entities;

namespace EzShare.Infrastructure.Database;

public class DatabaseSeeder(AppDbContext context)
{
    public async Task SeedData()
    {
        if (!await context.Database.CanConnectAsync() || context.Roles.Any()) return;
        var roles = new List<Role>
        {
            new() { Name = "Admin" },
            new() { Name = "User" }
        };
        await context.Roles.AddRangeAsync(roles);
        await context.SaveChangesAsync();
    }
}