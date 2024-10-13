using EzShare.Application.Contracts.Repositories;
using EzShare.Domain.Entities;
using EzShare.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EzShare.Infrastructure.Repositories;

public class RoleRepository(AppDbContext context) : GenericRepository<Role>(context), IRoleRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
    }
}