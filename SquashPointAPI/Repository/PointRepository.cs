using SquashPointAPI.Data;
using SquashPointAPI.Dto.Point;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class PointRepository(DataContext context) : IPointRepository
{
    public async Task<Point> CreatePointAsync(CreatePointDto createPointDto)
    {
        var point = createPointDto.ToPoint();
        await context.AddAsync(point);
        await context.SaveChangesAsync();
        return point;
    }

    public bool UpdateWinner(int playerId)
    {
        throw new NotImplementedException();
    }
}