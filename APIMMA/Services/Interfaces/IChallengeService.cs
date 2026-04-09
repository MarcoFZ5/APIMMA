using APIMMA.Models;
using APIMMA.Dtos.ChallengeDtos;

namespace APIMMA.Services.Interfaces
{
    public interface IChallengeService
    {
            Task<ChallengeDto> CreateChallengeAsync(ChallengeDto challenge);
            Task<IEnumerable<ChallengeDto>> GetAllChallengesAsync(int page, int pageSize);
            Task<ChallengeDto> GetChallengeByIdAsync(Guid id);
            // TO DO: ADD VALIDATION WITH FLUENT API
            Task<bool> UpdateChallengeAsync(Guid challengeId, ChallengeDto updatedChallenge);
            Task<bool> DeleteChallengeAsync(Guid id);
    }
}
