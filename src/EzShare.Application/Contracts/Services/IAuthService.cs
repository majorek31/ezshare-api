using EzShare.Domain.Entities;

namespace EzShare.Application.Contracts.Services;

public interface IAuthService
{
    Task<string> GenerateJwtAsync(User user);
}