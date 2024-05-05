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
            Winner = setModel.Winner == null ? null : $"{setModel.Winner.FirstName} {setModel.Winner.LastName}",
            CreatedAt = setModel.CreatedAt,
            Points = setModel.Points.Select(p => p.ToPointDto()).ToList(),
        };
    }
    
    public static Set ToSetFromCreateDto(this CreateSetDto createSetDto, Game game, Player? winner)
    {
        return new Set
        {
            Winner = winner,
            Game = game
        };
    }
}