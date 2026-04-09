using APIMMA.Dtos.UserDtos;

namespace APIMMA.Dtos.ChallengeDtos
{
    public record ChallengeParticipantDto (Guid ChallengeId, UserSimplifiedDto User, int Progress, byte Completed, DateTime JoinedAt);
}
