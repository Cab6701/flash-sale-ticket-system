using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticket.Domain.Entities;

namespace Ticket.Infrastructure.Persistence.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<EventTicket>
    {
        public void Configure(EntityTypeBuilder<EventTicket> builder)
        {
            builder.HasKey(t => t.Id);

            // Create Index for EventDate because user usually find ticket by event date
            builder.HasIndex(t => t.EventDate).HasDatabaseName("IX_EventTicket_EventDate");
            builder.Property(t => t.EventName).IsRequired().HasMaxLength(255);
            builder.Property(t => t.Price).HasColumnType("decimal(18,2)");
        }
    }
}
