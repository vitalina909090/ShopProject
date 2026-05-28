using Shop.DB.Entities.Identity;
using Shop.DB.Entities.Orders;
using Shop.DB.Entities.Catalog.Products;

namespace Shop.DB.Entities.Reviews
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public bool IsApproved { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int? OrderId { get; set; }

        public User User { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public Order? Order { get; set; }
    }
}
