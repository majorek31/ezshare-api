using System.Net;
using EzShare.Application.Common;
using EzShare.Application.Contracts.Repositories;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EzShare.Application.Features.User.Queries.UserInfoMe;

public class UserInfoMeQueryHandler(IUserRepository userRepository) : IRequestHandler<UserInfoMeQuery, Result<UserInfoResponse>>
{
    public async Task<Result<UserInfoResponse>> Handle(UserInfoMeQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetCurrentUserAsync();
        if (user is null) return Result<UserInfoResponse>.Failure("User not found", HttpStatusCode.NotFound);
        return Result<UserInfoResponse>.Success(user.Adapt<UserInfoResponse>(), HttpStatusCode.OK);
    }
}