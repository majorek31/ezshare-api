using EzShare.Domain.Entities;

namespace EzShare.Application.Contracts.Services;

public interface IAuthService
{
    Task<string> GenerateAccessToken(User user);
    Task<string> GenerateRefreshTokenAsync(User user);
}