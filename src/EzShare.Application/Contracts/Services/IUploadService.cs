using EzShare.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace EzShare.Application.Contracts.Services;

public interface IUploadService
{
    Task<Upload?> UploadAsync(User? user, IFormFile file);
    Task DeleteAsync(Upload upload);
}