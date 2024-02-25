using SquashPointAPI.Dto.Point;
using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface IPointRepository
{
    Task<Point> CreatePointAsync(CreatePointDto createPointDto);
    bool UpdateWinner(string playerId);
}