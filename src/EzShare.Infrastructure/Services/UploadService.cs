using EzShare.Application.Contracts.Repositories;
using EzShare.Application.Contracts.Services;
using EzShare.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace EzShare.Infrastructure.Services;

public class UploadService(IUploadRepository uploadRepository, IConfiguration configuration) : IUploadService
{
    public async Task<Upload?> UploadAsync(User? user, IFormFile file)
    {
        var uploadPath = configuration.GetValue("Upload:DataPath", "C:\\EzShare");
        if (user is null)
            uploadPath += "\\Anonymous";
        else uploadPath += $"\\{user.Id}";
        
        if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);
        var upload = await uploadRepository.AddAsync(new Upload
        {
            UserId = user?.Id,
            FileName = file.FileName,
            FileSize = file.Length,
            FileExtension = Path.GetExtension(file.FileName),
            MimeType = file.ContentType,
        });
        var filePath = Path.Combine(uploadPath, upload.Id.ToString());
        
        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        upload.FilePath = filePath;
        stream.Close();
        await uploadRepository.UpdateAsync(upload);
        
        return upload;
    }

    public async Task<byte[]> DownloadAsync(Upload upload)
    {
        if (!File.Exists(upload.FilePath))
            throw new FileNotFoundException("File not found.", upload.FilePath);
        return await File.ReadAllBytesAsync(upload.FilePath);
    }

    public async Task DeleteAsync(Upload upload)
    {
        
        throw new NotImplementedException();
    }
}