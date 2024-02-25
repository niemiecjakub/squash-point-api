using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Data;
using SquashPointAPI.Dto.Point;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class PointRepository(ApplicationDBContext context) : IPointRepository
{
    public async Task<Point> CreatePointAsync(CreatePointDto createPointDto)
    {
        var winner = await context.Players.FirstAsync(p => p.Id.Equals(createPointDto.WinnerId));
        var set = await context.Set.FirstAsync(s => s.Id == createPointDto.SetId);
        var point = createPointDto.ToPointFromCreateDto(winner, set);

        await context.AddAsync(point);
        await context.SaveChangesAsync();
        return point;
    }

    public bool UpdateWinner(string playerId)
    {
        throw new NotImplementedException();
    }
}