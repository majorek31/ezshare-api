using EzShare.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EzShare.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet("me")]
    public async Task<ActionResult<Result<UserInfoResponse>>> GetMe()
    {
        var result = await mediator.Send(new UserInfoMeQuery());
        return StatusCode((int)result.StatusCode, result);
    }
}