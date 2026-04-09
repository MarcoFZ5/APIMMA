using APIMMA.Dtos.ChallengeDtos;

namespace APIMMA.Services.Interfaces
{
    public interface IChallengeParticipantService
    {
        Task<bool> AddParticipant(Guid challengeId, Guid userId);

        Task<bool> RemoveParticipant(Guid userId, Guid challengeId);

        Task<UpdateProgressResultDto> AddProgress(Guid challengeParticipantId, UpdateChallengeParticipantDto progressDto);
    }
}
