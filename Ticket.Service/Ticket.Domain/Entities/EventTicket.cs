namespace Ticket.Domain.Entities
{
    public class EventTicket
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public decimal Price { get; set; }
        public int TotalSlots { get; set; }
        public int AvailableSlots { get; set; }
        public string Location { get; set; }
        public bool IsPublished { get; set; }
    }
}
