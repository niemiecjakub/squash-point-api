using SquashPointAPI.Dto.Point;
using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface IPointRepository
{
    Task<Point> CreatePointAsync(Point point);
}