using SquashPointAPI.Dto.Set;
using SquashPointAPI.Models;

namespace SquashPointAPI.Mappers;

public static class SetMapper
{
    public static SetDto ToSetDto(this Set setModel)
    {
        return new SetDto
        {
            Id = setModel.Id,
            Winner = setModel.Winner.ToPlayerDto(),
            CreatedAt = setModel.CreatedAt,
            Points = setModel.Points.Select(p => p.ToPointDto()).ToList()
        };
    }
}