using CarAdverts.Application.CarAdvert.Commands;
using CarAdverts.Application.CarAdvert.Dtos;
using CarAdverts.Application.CarAdvert.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarAdverts.Net7.Controllers;

[Route("api/car-adverts")]
[ApiController]
public class CarAdvertController : ControllerBase
{
    private readonly IMediator _mediator;

    public CarAdvertController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id:guid}", Name = "GetById")]
    public async Task<ActionResult<CarAdvertDto>> Get(Guid id)
    {
        var response = await _mediator.Send(new GetByIdQuery { Id = id }).ConfigureAwait(false);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarAdvertDto>>> Get([FromQuery] GetAllQuery getAllQuery)
    {
        var response = await _mediator.Send(getAllQuery).ConfigureAwait(false);

        return Ok(response.ToArray());
    }

    [HttpPost]
    public async Task<ActionResult<CarAdvertDto>> Post([FromBody] CreateCommand createCommand)
    {
        var response = await _mediator.Send(createCommand).ConfigureAwait(false);

        return CreatedAtRoute("GetById", new { response.Id }, response);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] CreateCommand createCommand)
    {
        await _mediator.Send(new UpdateCommand(id, createCommand)).ConfigureAwait(false);

        return Ok();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteCommand { Id = id }).ConfigureAwait(false);

        return Ok();
    }
}
