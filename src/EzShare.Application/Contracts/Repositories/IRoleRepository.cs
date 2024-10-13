using EzShare.Domain.Entities;

namespace EzShare.Application.Contracts.Repositories;

public interface IRoleRepository : IGenericRepository<Role>
{
    Task<Role?> GetByNameAsync(string name);
}