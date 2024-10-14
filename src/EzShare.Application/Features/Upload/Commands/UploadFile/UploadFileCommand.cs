using EzShare.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EzShare.Application.Features.Upload.Commands.UploadFile;

public record UploadFileResponse(Guid Id, string FileName, string FileExtension, long FileSize, string MimeType);
public record UploadFileCommand(IFormFile? File) : IRequest<Result<UploadFileResponse>>;