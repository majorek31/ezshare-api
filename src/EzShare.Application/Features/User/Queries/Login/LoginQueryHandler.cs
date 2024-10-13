using System.Net;
using EzShare.Application.Common;
using EzShare.Application.Contracts.Repositories;
using EzShare.Application.Contracts.Services;
using MediatR;

namespace EzShare.Application.Features.User.Queries.Login;

public class LoginQueryHandler(IAuthService authService, IUserRepository userRepository) : IRequestHandler<LoginQuery, Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAndPasswordAsync(request.Dto.Email, request.Dto.Password);
        if (user is null) return Result<LoginResponse>.Failure("Invalid email or password", HttpStatusCode.Unauthorized);

        return Result<LoginResponse>.Success(new LoginResponse(
            AccessToken: await authService.GenerateAccessToken(user),
            RefreshToken: await authService.GenerateRefreshTokenAsync(user)
        ), HttpStatusCode.OK);
    }
}