
namespace Shop.DB.Entities.Support
{
    public class SupportMessage
    {
        public int Id { get; set; }
        public int SupportTicketId { get; set; }
        public int Sender { get; set; }
        public string Message { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }

        public SupportTicket SupportTicket { get; set; } = null!;
    }
}
