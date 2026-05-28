using Shop.DB.Entities.Catalog.Products;
using Shop.DB.Entities.Identity;

namespace Shop.DB.Entities.Wishlists
{
    public class Wishlist
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public User User { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
