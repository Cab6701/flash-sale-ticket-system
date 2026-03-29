using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Entities;

namespace Ticket.Application.Interfaces
{
    public interface ITicketDbContext
    {
        DbSet<EventTicket> EventTickets { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
