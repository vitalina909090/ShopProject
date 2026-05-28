using Shop.DB.Entities.Reviews;
using Shop.DB.Entities.Support;
using Shop.DB.Entities.Carts;
using Shop.DB.Entities.Orders;
using Shop.DB.Entities.Wishlists;

namespace Shop.DB.Entities.Identity
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public int RoleId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsBlocked { get; set; }
        public string? BlockReason { get; set; }

        public Role Role { get; set; } = null!;
        public CustomerProfile? CustomerProfile { get; set; }
        public PersonnelProfile? PersonnelProfile { get; set; }
        public Cart? Cart { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();
        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
    }
}
