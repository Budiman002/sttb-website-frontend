using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sttb.Contracts.RequestModels.Media;

namespace Sttb.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaController : ControllerBase
{
    private readonly IMediator _mediator;

    public MediaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("list")]
    public async Task<IActionResult> List(
        [FromQuery] GetMediaListRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("artikel/{slug}")]
    public async Task<IActionResult> ArtikelDetail(
        string slug,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(
                new GetMediaArtikelDetailRequest { Slug = slug },
                cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("video/{slug}")]
    public async Task<IActionResult> VideoDetail(
        string slug,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(
                new GetMediaVideoDetailRequest { Slug = slug },
                cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("artikel")]
    [Authorize]
    public async Task<IActionResult> CreateArtikel(
        [FromBody] CreateMediaArtikelRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var id = await _mediator.Send(request, cancellationToken);
            return Ok(id);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("artikel/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateArtikel(
        Guid id,
        [FromBody] UpdateMediaArtikelRequest request,
        CancellationToken cancellationToken)
    {
        request.Id = id;
        try
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("artikel/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteArtikel(
        Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(
                new DeleteMediaArtikelRequest { Id = id },
                cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("video")]
    [Authorize]
    public async Task<IActionResult> CreateVideo(
        [FromBody] CreateMediaVideoRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var id = await _mediator.Send(request, cancellationToken);
            return Ok(id);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("video/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateVideo(
        Guid id,
        [FromBody] UpdateMediaVideoRequest request,
        CancellationToken cancellationToken)
    {
        request.Id = id;
        try
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("video/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteVideo(
        Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(
                new DeleteMediaVideoRequest { Id = id },
                cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
