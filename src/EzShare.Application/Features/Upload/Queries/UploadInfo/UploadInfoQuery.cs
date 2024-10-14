using EzShare.Application.Common;
using MediatR;

namespace EzShare.Application.Features.Upload.Queries.UploadInfo;

public record UploadInfoResponse(string FileName, string FileExtension, long FileSize, string MimeType, UserInfoResponse? Uploader); 

public record UploadInfoQuery(Guid Id) : IRequest<Result<UploadInfoResponse>>;

