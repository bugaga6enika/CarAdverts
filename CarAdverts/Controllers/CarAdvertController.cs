using CarAdverts.Application.CarAdvert.Commands;
using CarAdverts.Application.CarAdvert.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CarAdverts.Controllers
{
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
        [Route("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _mediator.Send(new GetByIdQuery { Id = id });

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllQuery getAllQuery)
        {
            var response = await _mediator.Send(getAllQuery);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCommand createCommand)
        {
            var response = await _mediator.Send(createCommand);

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CreateCommand createCommand)
        {
            await _mediator.Send(new UpdateCommand(id, createCommand));

            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCommand { Id = id });

            return Ok();
        }
    }
}