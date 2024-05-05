using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Data;
using SquashPointAPI.Dto.Point;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class PointRepository(ApplicationDBContext context) : IPointRepository
{
    public async Task<Point> CreatePointAsync(Point point)
    {
        await context.AddAsync(point);
        await context.SaveChangesAsync();
        return point;
    }

}