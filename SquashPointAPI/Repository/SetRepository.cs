using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Data;
using SquashPointAPI.Dto.Set;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class SetRepository(DataContext context) : ISetRepository
{
    public async Task<Set> CreateSetAsync(CreateSetDto setCreate)
    {
        var game = await context.Games.FirstAsync(g => g.Id == setCreate.GameId);
        var winner = await context.Players.FirstOrDefaultAsync(p => p.Id == setCreate.WinnerId);
        var set = setCreate.ToSetFromCreateDto(game, winner);

        await context.AddAsync(set);
        await context.SaveChangesAsync();

        return set;
    }

    public async Task<Set> UpdateWinnerAsync(int setId, UpdateSetRequestDto updateDto)
    {
        var set = await context.Set.FirstAsync(s => s.Id == setId);
        set.Winner = await context.Players.FirstAsync(p => p.Id == updateDto.WinnerId);
        
        await context.SaveChangesAsync();

        return set;
    }

    public async Task<bool> SetExistsAsync(int setId)
    {
        return await context.Set.AnyAsync(s => s.Id == setId);
    }
}