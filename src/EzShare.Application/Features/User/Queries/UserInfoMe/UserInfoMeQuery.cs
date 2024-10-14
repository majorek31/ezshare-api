using EzShare.Application.Common;
using MediatR;
public record UserInfoResponse(string Email, string RoleName);

public record UserInfoMeQuery() : IRequest<Result<UserInfoResponse>>;

