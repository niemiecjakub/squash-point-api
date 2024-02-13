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
            PointType = pointModel.PointType,
            Winner = pointModel.Winner.ToPlayerDto(),
            CreatedAt = pointModel.CreatedAt,
        };
    }
}