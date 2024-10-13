using EzShare.Domain.Entities;

namespace EzShare.Application.Contracts.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByEmailAndPasswordAsync(string email, string password);
    Task<bool> IsEmailUniqueAsync(string email);
    Task<User?> GetByRefreshTokenAsync(string refreshToken);
}