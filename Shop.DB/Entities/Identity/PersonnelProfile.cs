
namespace Shop.DB.Entities.Identity
{
    public class PersonnelProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Department { get; set; }

        public User User { get; set; } = null!;
    }
}
