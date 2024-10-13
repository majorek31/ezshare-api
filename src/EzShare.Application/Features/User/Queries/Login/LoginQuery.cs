using EzShare.Application.Common;
using MediatR;

namespace EzShare.Application.Features.User.Queries.Login;

public record LoginResponse(string AccessToken, string RefreshToken);
public record LoginDto(string Email, string Password);

public record LoginQuery(LoginDto Dto) : IRequest<Result<LoginResponse>>;
