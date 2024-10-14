using System.Net;
using EzShare.Application.Common;
using EzShare.Application.Contracts.Repositories;
using EzShare.Application.Contracts.Services;
using MediatR;

namespace EzShare.Application.Features.Upload.Queries.Download;

public class DownloadFileQueryHandler(IUploadService uploadService, IUploadRepository uploadRepository) : IRequestHandler<DownloadFileQuery, Result<DownloadFileResponse>>
{
    public async Task<Result<DownloadFileResponse>> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
    {
        var file = await uploadRepository.GetByIdAsync(request.Id);
        if (file is null) return Result<DownloadFileResponse>.Failure("File not found.", HttpStatusCode.NotFound);
        var fileContent = await uploadService.DownloadAsync(file);
        return Result<DownloadFileResponse>.Success(new DownloadFileResponse(
            fileContent,
            file.FileName,
            file.MimeType
        ), HttpStatusCode.OK);
    }
}