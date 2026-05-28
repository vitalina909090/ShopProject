using Shop.DB.Entities.Orders;

namespace Shop.DB.Entities.Payments
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = null!;
        public int Status { get; set; }
        public int? ProviderId { get; set; }
        public string? ProviderPaymentId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public Order Order { get; set; } = null!;
        public Provider? Provider { get; set; }
    }
}
