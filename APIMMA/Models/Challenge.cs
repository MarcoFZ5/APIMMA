namespace APIMMA.Models
{
    public class Challenge
    {
        Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // A Challenge has many participants

        public List<ChallengeParticipant> Participants { get; set; }

    }
}
