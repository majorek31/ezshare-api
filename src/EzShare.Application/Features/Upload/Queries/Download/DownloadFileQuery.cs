using EzShare.Application.Common;
using MediatR;

namespace EzShare.Application.Features.Upload.Queries.Download;
public record DownloadFileResponse(byte[] FileContent, string FileName, string MimeType);
public record DownloadFileQuery(Guid Id) : IRequest<Result<DownloadFileResponse>>;
