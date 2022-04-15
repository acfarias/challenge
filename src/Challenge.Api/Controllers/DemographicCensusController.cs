using Challenge.Domain.Notifications;
using Challenge.Services.Dtos.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenge.Api.Controllers
{
    [ApiController]
    [Route("census")]
    public class DemographicCensusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DemographicCensusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), 201)]
        [ProducesResponseType(typeof(List<Notification>), 400)]
        public async Task<IActionResult> Create([FromBody] CreateCensusCommand command)
        {
            return Created(string.Empty, await _mediator.Send(command));
        }
    }
}
