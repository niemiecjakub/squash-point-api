using SquashPointAPI.Dto.Point;
using SquashPointAPI.Dto.Set;
using SquashPointAPI.Models;

namespace SquashPointAPI.Mappers;

public static class PointMapper
{
    public static PointDto ToPointDto(this Point pointModel)
    {
        return new PointDto
        {
            Id = pointModel.Id,
            PointType = pointModel.PointType,
            Winner = pointModel.Winner.ToPlayerDto(),
            CreatedAt = pointModel.CreatedAt,
        };
    }
    
    public static Point ToPointFromCreateDto(this CreatePointDto createPointDto, Player winner, Set set)
    {
        return new Point()
        {
            PointType = createPointDto.PointType,
            Winner = winner,
            Set = set,
        };
    }
}