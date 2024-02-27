using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Data;
using SquashPointAPI.Dto.Set;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class SetRepository(ApplicationDBContext context) : ISetRepository
{
    public async Task<Set> GetSetAsync(int setId)
    {
        return await context.Set.FirstOrDefaultAsync(s => s.Id == setId);
    }

    public async Task<Set> CreateSetAsync(Set set)
    {
        await context.AddAsync(set);
        await context.SaveChangesAsync();
        return set;
    }

    public async Task<Set> UpdateWinnerAsync(int setId, UpdateSetRequestDto updateDto)
    {
        var set = await context.Set.FirstAsync(s => s.Id == setId);
        set.Winner = await context.Players.FirstAsync(p => p.Id.Equals(updateDto.WinnerId));

        await context.SaveChangesAsync();

        return set;
    }

    public async Task<bool> SetExistsAsync(int setId)
    {
        return await context.Set.AnyAsync(s => s.Id == setId);
    }
}