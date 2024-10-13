using EzShare.Application.Common;
using MediatR;

namespace EzShare.Application.Features.User.Commands.Register;

public record RegisterDto(string Email, string Password);

public record RegisterCommand(RegisterDto Dto) : IRequest<Result<Unit>>;