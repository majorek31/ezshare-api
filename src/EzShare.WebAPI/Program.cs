using EzShare.Application;
using EzShare.Infrastructure;
using EzShare.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

builder.Services
    .AddApplication()
    .AddInfrastructure(configuration);

var app = builder.Build();

var scope = app.Services.CreateScope();

var seeder = scope.ServiceProvider.GetService<DatabaseSeeder>();
await seeder?.SeedData()!;

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();