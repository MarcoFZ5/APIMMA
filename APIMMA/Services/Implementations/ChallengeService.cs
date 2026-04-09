using APIMMA.Data;
using APIMMA.Dtos.ChallengeDtos;
using APIMMA.Exceptions;
using APIMMA.Models;
using APIMMA.Services.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace APIMMA.Services.Implementations
{
    public class ChallengeService : IChallengeService
    {
        public readonly AppDbContext _context;
        public ChallengeService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ChallengeDto>> GetAllChallengesAsync(int page, int pageSize)
        {
            var challenges = await _context.Challenges
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var challengesDtos = challenges.Adapt<IEnumerable<ChallengeDto>>(); // AUTO MAPP ALL CHALLENGES TO DTOS

            return challengesDtos;
        }
        public async Task<ChallengeDto> GetChallengeByIdAsync(Guid id)
        {
            var challenge = await _context.Challenges.FindAsync(id);

            if (challenge == null) throw new NotFoundException($"Challenge with ID {id} not found.");

            return challenge.Adapt<ChallengeDto>();
        }

        public async Task<ChallengeDto> CreateChallengeAsync(ChallengeDto challenge)
        {
            var newChallenge = new Challenge
            {
                Title = challenge.Title,
                Description = challenge.Description,
                StartDate = challenge.StartDate,
                EndDate = challenge.EndDate
            };

            _context.Challenges.Add(newChallenge);
            await _context.SaveChangesAsync();

            return newChallenge.Adapt<ChallengeDto>();
        }

        public async Task<bool> DeleteChallengeAsync(Guid id)
        {
            var challenge = await _context.Challenges.FindAsync(id);

            if (challenge == null) throw new NotFoundException($"Challenge with ID {id} not found.");

            _context.Challenges.Remove(challenge);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateChallengeAsync(Guid challengeId, ChallengeDto updatedChallenge)
        {
            var challenge = await _context.Challenges.FindAsync(challengeId);

            if (challenge == null) throw new NotFoundException($"Challenge with ID {challengeId} not found.");

            challenge.Title = updatedChallenge.Title;
            challenge.Description = updatedChallenge.Description;
            challenge.StartDate = updatedChallenge.StartDate;
            challenge.EndDate = updatedChallenge.EndDate;

            _context.Challenges.Update(challenge);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}



