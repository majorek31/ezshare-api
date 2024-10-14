using System.Net;
using EzShare.Application.Common;
using EzShare.Application.Contracts.Repositories;
using EzShare.Application.Features.Upload.Commands.UploadFile;
using Mapster;
using MediatR;

namespace EzShare.Application.Features.Upload.Queries.UploadInfo;

public class UploadInfoQueryHandler(IUserRepository userRepository, IUploadRepository uploadRepository) : IRequestHandler<UploadInfoQuery, Result<UploadInfoResponse>>
{
    public async Task<Result<UploadInfoResponse>> Handle(UploadInfoQuery request, CancellationToken cancellationToken)
    {
        var upload = await uploadRepository.GetByIdAsync(request.Id);
        if (upload is null) return Result<UploadInfoResponse>.Failure("Upload not found.", HttpStatusCode.NotFound);
        Domain.Entities.User? uploader = null;
        if (upload.UserId is not null)
        {
            uploader = await userRepository.GetByIdAsync((Guid)upload.UserId);
        }
        var response = upload.Adapt<UploadInfoResponse>();
        response = response with { Uploader = uploader?.Adapt<UserInfoResponse>() };
        return Result<UploadInfoResponse>.Success(response, HttpStatusCode.OK);
    }
}