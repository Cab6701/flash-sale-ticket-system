using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Features.Tickets.Queries;

namespace Ticket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        private readonly IMediator _mediator;
        public TicketController(IMediator mediatR)
        {
            _mediator = mediatR;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableTickets()
        {
            var query = new GetAvailableTicketsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
