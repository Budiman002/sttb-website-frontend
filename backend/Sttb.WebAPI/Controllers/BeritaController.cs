using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sttb.Contracts.RequestModels.Berita;

namespace Sttb.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BeritaController : ControllerBase
{
    private readonly IMediator _mediator;

    public BeritaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("list")]
    public async Task<IActionResult> List(
        [FromQuery] GetBeritaListRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> Detail(
        string slug,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(
                new GetBeritaDetailRequest { Slug = slug },
                cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(
        [FromBody] CreateBeritaRequest request,
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

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateBeritaRequest request,
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

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(
                new DeleteBeritaRequest { Id = id },
                cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
