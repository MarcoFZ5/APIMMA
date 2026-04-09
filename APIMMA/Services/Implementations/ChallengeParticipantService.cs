using APIMMA.Data;
using APIMMA.Dtos.ChallengeDtos;
using APIMMA.Models;
using APIMMA.Services.Interfaces;

namespace APIMMA.Services.Implementations
{
    public class ChallengeParticipantService : IChallengeParticipantService
    {
        private readonly AppDbContext _context;

        public ChallengeParticipantService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UpdateProgressResultDto> AddProgress(Guid challengeParticipantId, UpdateChallengeParticipantDto progressDto)
        {
            var challengeParticipant = await _context.ChallengeParticipants.FindAsync(challengeParticipantId);

            if (challengeParticipant == null) throw new KeyNotFoundException("Challenge participant not found.");

            // CHECK IF WAS ALREADY COMPLETED
            var wasCompleted = challengeParticipant.Completed == 1;

            // CHECK NEW PROGRESS
            var newProgress = challengeParticipant.Progress + progressDto.Progress;

            // 100 TOP CAP
            if (newProgress > 100) newProgress = 100;

            // IF >100 NOW IS COMPLETED
            var isNowCompleted = newProgress >= 100;

            // CHECK IF CHALLENGE JUST GOT COMPLETED
            var justCompleted = !wasCompleted && isNowCompleted;

            challengeParticipant.Progress = newProgress;
            challengeParticipant.Completed = isNowCompleted ? (byte)1 : (byte)0;

            await _context.SaveChangesAsync();

            var response = new UpdateProgressResultDto (justCompleted, newProgress, isNowCompleted);

            return response;
        }

        public async Task<bool> AddParticipant(Guid challengeId, Guid userId)
        {
            var newParticipant = new ChallengeParticipant
            {
                ChallengeId = challengeId,
                UserId = userId,
                Progress = 0,
                Completed = 0,
                JoinedAt = DateTime.UtcNow
            };

            _context.ChallengeParticipants.Add(newParticipant);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<bool> RemoveParticipant(Guid userId, Guid challengeId)
        {
            throw new NotImplementedException();
        }
    }
}
