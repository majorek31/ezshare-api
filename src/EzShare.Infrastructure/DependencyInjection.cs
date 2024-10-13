using EzShare.Application.Contracts.Repositories;
using EzShare.Infrastructure.Database;
using EzShare.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EzShare.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(x => 
            x.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<DatabaseSeeder>();
        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUploadRepository, UploadRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        
        return services;
    }
}