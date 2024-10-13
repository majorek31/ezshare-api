using System.Net;
using EzShare.Application.Common;
using EzShare.Application.Contracts.Repositories;
using EzShare.Application.Contracts.Services;
using EzShare.Application.Features.User.Queries.Login;
using MediatR;

namespace EzShare.Application.Features.User.Queries.RefreshToken;

public class RefreshTokenQueryHandler(IAuthService authService, IUserRepository userRepository) : IRequestHandler<RefreshTokenQuery, Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByRefreshTokenAsync(request.RefreshToken);
        if (user is null) return Result<LoginResponse>.Failure("Invalid refresh token", HttpStatusCode.BadRequest);
        
        return Result<LoginResponse>.Success(new LoginResponse(
            AccessToken: await authService.GenerateAccessToken(user),
            RefreshToken: await authService.GenerateRefreshTokenAsync(user)
        ), HttpStatusCode.OK);
    }
}