using MediatR;
using Microsoft.EntityFrameworkCore;
using Ticket.Application.Interfaces;
using Ticket.Domain.Entities;

namespace Ticket.Application.Features.Tickets.Queries
{
    public record GetAvailableTicketsQuery : IRequest<List<EventTicket>>;
    public class GetAvailableTicketsHandler : IRequestHandler<GetAvailableTicketsQuery, List<EventTicket>>
    {
        private readonly ITicketDbContext _context;
        public GetAvailableTicketsHandler(ITicketDbContext context) => _context = context;
        public async Task<List<EventTicket>> Handle(GetAvailableTicketsQuery request, CancellationToken cancellationToken)
        {
            return await _context.EventTickets
            .Where(t => t.AvailableSlots > 0 && t.IsPublished)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        }
    }
}
