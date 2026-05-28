using Shop.DB.Entities.Identity;
using Shop.DB.Entities.Orders;

namespace Shop.DB.Entities.Support
{
    public class SupportTicket
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Topic { get; set; } = null!;
        public int Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int? OrderId { get; set; }
        public string? GuestEmail { get; set; }
        public string? GuestPhone { get; set; }

        public User? User { get; set; }
        public Order? Order { get; set; }
        public ICollection<SupportMessage> Messages { get; set; } = new List<SupportMessage>();
    }
}
