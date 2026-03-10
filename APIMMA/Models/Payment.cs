namespace APIMMA.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime Paid_at { get; set; } = DateTime.UtcNow;
        public string Status { get; set; }

        public string Payment_method { get; set; }

        // Navigation properties
        // One payment belongs to one membership
        public int Membership_id { get; set; }
        public Membership Membership { get; set; }
    }
}
