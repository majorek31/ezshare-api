using System.Net;
using EzShare.Application.Common;
using EzShare.Application.Contracts.Repositories;
using EzShare.Application.Contracts.Services;
using Mapster;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EzShare.Application.Features.Upload.Commands.UploadFile;

public class UploadFileCommandHandler(IUserRepository userRepository, IUploadService uploadService, IConfiguration configuration) : IRequestHandler<UploadFileCommand, Result<UploadFileResponse>>
{
    public async Task<Result<UploadFileResponse>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetCurrentUserAsync();
        var maxAnonFileSize = configuration.GetValue("Upload:MaxAnonFileSize", 1 << 30);
        var maxAuthFileSize = configuration.GetValue("Upload:MaxAuthFileSize", unchecked((1 << 30) * 5));
        var formFile = request.File;
        if (formFile is null) return Result<UploadFileResponse>.Failure("No file uploaded.", HttpStatusCode.BadRequest);
        if (user is null && formFile.Length > maxAnonFileSize)
            return Result<UploadFileResponse>.Failure(
                $"Not logged in users can upload file of max size: {maxAnonFileSize} bytes.",
                HttpStatusCode.BadRequest);
        if (formFile.Length > maxAuthFileSize) return Result<UploadFileResponse>.
            Failure (
                $"Logged in users can upload file of max size: {maxAuthFileSize} bytes.",
                HttpStatusCode.BadRequest);
        var upload = await uploadService.UploadAsync(user, formFile);
        return Result<UploadFileResponse>.Success(upload.Adapt<UploadFileResponse>(), HttpStatusCode.Created);
    }
}