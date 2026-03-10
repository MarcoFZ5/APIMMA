namespace APIMMA.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime Created_at { get; set; } = DateTime.UtcNow;

        // Navigation properties
        // One booking belongs to one user
        public int User_id { get; set; }
        public User User { get; set; }

        // One booking belongs to one class
        public int Class_id { get; set; }
        public Classes ClassBooked { get; set; }

    }
}
