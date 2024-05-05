using SquashPointAPI.Dto.Point;
using SquashPointAPI.Models;

namespace SquashPointAPI.Mappers;

public static class PointMapper
{
    public static PointDto ToPointDto(this Point pointModel)
    {
        return new PointDto
        {
            Id = pointModel.Id,
            Player = pointModel.Player.ToString(),
            Points = pointModel.PointScore,
        };
    }

    public static Point ToPointFromCreateDto(this CreatePointDto createPointDto, Player player, Set set)
    {
        return new Point
        {
            Player = player,
            Set = set,
            PointScore = createPointDto.Points
        };
    }
}