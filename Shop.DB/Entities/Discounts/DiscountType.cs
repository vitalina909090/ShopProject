
namespace Shop.DB.Entities.Discounts
{
    public class DiscountType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    }
}
