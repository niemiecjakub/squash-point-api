using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Data;
using SquashPointAPI.Dto.Set;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class SetRepository(DataContext context) : ISetRepository
{
    public async Task<Set> CreateSetAsync(CreateSetDto createSetDto)
    {
        var game = await context.Games.FindAsync(createSetDto.GameId);
        var set = new Set()
        {
            Game = game,
            Winner = null
        };
        
        context.Set.Add(set);
        await context.SaveChangesAsync();
        
        return set;
    }

    public async Task<Set> UpdateWinnerAsync(int playerId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetExistsAsync(int setId)
    {
        return await context.Set.AnyAsync(s => s.Id == setId);
    }
}