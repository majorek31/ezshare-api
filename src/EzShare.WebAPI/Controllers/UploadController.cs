using EzShare.Application.Features.Upload.Commands.UploadFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EzShare.WebAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UploadController(IMediator mediator) : ControllerBase
{
    private const long MaxFileSize = 1073741824L * 5; // 5 GB
    [HttpPost]
    [RequestSizeLimit(MaxFileSize)]
    public async Task<ActionResult<UploadFileResponse>> UploadFile(IFormFile? file)
    {
        var result = await mediator.Send(new UploadFileCommand(file));
        return StatusCode((int)result.StatusCode, result);
    }
}