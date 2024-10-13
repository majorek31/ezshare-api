using EzShare.Application.Common;
using EzShare.Application.Features.User.Queries.Login;
using MediatR;

namespace EzShare.Application.Features.User.Queries.RefreshToken;

public record RefreshTokenQuery(string RefreshToken) : IRequest<Result<LoginResponse>>;
