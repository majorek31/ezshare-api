using EzShare.Application.Features.Upload.Commands.UploadFile;
using EzShare.Application.Features.Upload.Queries.UploadInfo;
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
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UploadInfoResponse>> GetUploadInfo(Guid id)
    {
        var result = await mediator.Send(new UploadInfoQuery(id));
        return StatusCode((int)result.StatusCode, result);
    }
    
}