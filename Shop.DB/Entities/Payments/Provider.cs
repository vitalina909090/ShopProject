
namespace Shop.DB.Entities.Payments 
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
