using Shop.DB.Entities.Identity;

namespace Shop.DB.Entities.Support
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; } = null!;
        public int Type { get; set; }
        public string? Url { get; set; }
        public bool IsRead { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public User User { get; set; } = null!;
    }
}
