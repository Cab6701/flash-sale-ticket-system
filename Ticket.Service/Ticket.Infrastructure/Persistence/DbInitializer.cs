using Ticket.Domain.Entities;
using Ticket.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task SeedData(TicketDbContext context)
    {
        if (context.EventTickets.Any()) return;

        var tickets = new List<EventTicket>();
        var random = new Random();
        var locations = new[] { "Hà Nội", "TP. Hồ Chí Minh", "Đà Nẵng", "Cần Thơ", "Hải Phòng" };

        for (int i = 1; i <= 10000; i++)
        {
            tickets.Add(new EventTicket
            {
                Id = Guid.NewGuid(),
                EventName = $"Sự kiện âm nhạc số {i}",
                EventDate = DateTime.Now.AddDays(random.Next(1, 365)), // Random ngày trong năm tới
                Price = random.Next(100, 2000) * 1000, // Giá từ 100k - 2tr
                TotalSlots = 500,
                AvailableSlots = random.Next(0, 500),
                Location = locations[random.Next(locations.Length)],
                IsPublished = true
            });
        }

        await context.EventTickets.AddRangeAsync(tickets);
        await context.SaveChangesAsync();
    }
}