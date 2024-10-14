using System.Security.Claims;
using System.Text;
using EzShare.Application.Contracts.Repositories;
using EzShare.Application.Contracts.Services;
using EzShare.Infrastructure.Database;
using EzShare.Infrastructure.Repositories;
using EzShare.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EzShare.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        
        services.AddDbContext<AppDbContext>(x => 
            x.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<DatabaseSeeder>();

        services.AddHttpContextAccessor();
        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUploadRepository, UploadRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddScoped<IAuthService, AuthService>();

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:PrivateKey"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
        });

        services.AddAuthorization(x =>
        {
            x.AddPolicy("AuthUser", p => p.RequireClaim(ClaimTypes.Role, "User", "Admin"));
            x.AddPolicy("AuthAdmin", p => p.RequireClaim(ClaimTypes.Role, "Admin"));
        });

        services.Configure<FormOptions>(x =>
        {
            x.ValueLengthLimit = int.MaxValue;
            x.MultipartBodyLengthLimit = 1073741824L * 5;
        });
        
        return services;
    }
}