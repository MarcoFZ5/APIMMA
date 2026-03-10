namespace APIMMA.Models
{
    public class Classes
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public DateTime Date { get; set; }

        // Navigation properties
        // One class belongs to one coach (user)
        public int Coach_id { get; set; }
        public User User { get; set; }

        // One class can have many bookings
        public ICollection<Booking> Bookings { get; set; }
    }
}
