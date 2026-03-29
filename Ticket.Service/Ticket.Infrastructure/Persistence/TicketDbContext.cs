using Microsoft.EntityFrameworkCore;
using Ticket.Application.Interfaces;
using Ticket.Domain.Entities;

namespace Ticket.Infrastructure.Persistence
{
    public class TicketDbContext : DbContext, ITicketDbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) 
            : base(options)
        {
        }
        public DbSet<EventTicket> EventTickets => Set<EventTicket>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TicketDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
