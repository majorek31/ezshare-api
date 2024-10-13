using EzShare.Application.Common;
using EzShare.Application.Features.User.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EzShare.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<Result<Unit>>> Register([FromBody] RegisterDto command)
    {
        var result = await mediator.Send(new RegisterCommand(command));
        return StatusCode((int)result.StatusCode, result);
    }
}