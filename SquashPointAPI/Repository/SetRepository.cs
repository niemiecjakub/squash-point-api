using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Data;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class SetRepository(DataContext context) : ISetRepository
{
    public async Task<Set> CreateSetAsync(int gameId)
    {
        var game = await context.Games.Where(g => g.Id == gameId).FirstOrDefaultAsync();
        var set = new Set()
        {
            Game = game,
            Winner = null
        };
        
        await context.Set.AddAsync(set);
        await context.SaveChangesAsync();
        return set;
    }

    public async Task<Set> UpdateWinnerAsync(int playerId)
    {
        throw new NotImplementedException();
    }
}