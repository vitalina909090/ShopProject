using Shop.DB.Entities.Reviews;
using Shop.DB.Entities.Identity;
using Shop.DB.Entities.Support;
using Shop.DB.Entities.Payments;

namespace Shop.DB.Entities.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public int Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CustomerEmail { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string? CustomerPhone { get; set; }
        public string ShippingAddress { get; set; } = null!;
        public string? StripePaymentIntentId { get; set; }
        public string? PostalCode { get; set; }

        public User? User { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();
    }
}
