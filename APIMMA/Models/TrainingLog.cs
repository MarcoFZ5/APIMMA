namespace APIMMA.Models
{
    public class TrainingLog
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public int Duration { get; set; } // Duration in minutes
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relation with user, a training log belongs to a users
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
