using System.ComponentModel;

namespace APIMMA.Models
{
    public class ChallengeParticipant
    {
        Guid Id { get; set; }
        public int Progress { get; set; } 

        public byte Completed { get; set; }

        public DateTime JoinedAt { get; set; }

        // Connections
        public Guid ChallengeId { get; set; }
        public Challenge Challenge { get; set; }

        public Guid UserId { get; set; }
        public User UserParticipant { get; set; }
    }
}
