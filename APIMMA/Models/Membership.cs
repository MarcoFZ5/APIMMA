namespace APIMMA.Models
{
    public class Membership
    {
        public int Id { get; set; }
        public string Plan_type { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public string Status { get; set; }

        // Navigation properties
        // One membership belongs to one user
        public int User_id { get; set; }
        public User User { get; set; }

        // One membership can have many paymentss
        public ICollection<Payment> Payments { get; set; }

    }
}
