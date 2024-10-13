using EzShare.Application.Common;
using EzShare.Application.Features.User.Commands.Register;
using EzShare.Application.Features.User.Queries.Login;
using EzShare.Application.Features.User.Queries.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EzShare.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<Result<Unit>>> Register([FromBody] RegisterDto command)
    {
        var result = await mediator.Send(new RegisterCommand(command));
        return StatusCode((int)result.StatusCode, result);
    }
    [HttpGet("login")]
    public async Task<ActionResult<Result<LoginResponse>>> Login([FromQuery] LoginDto query)
    {
        var result = await mediator.Send(new LoginQuery(query));
        return StatusCode((int)result.StatusCode, result);
    }
    [HttpGet("refresh-token")]
    public async Task<ActionResult<Result<LoginResponse>>> RefreshToken([FromQuery] string refreshToken)
    {
        var result = await mediator.Send(new RefreshTokenQuery(refreshToken));
        return StatusCode((int)result.StatusCode, result);
    }
}