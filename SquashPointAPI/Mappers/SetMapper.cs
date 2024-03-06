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
            Winner = setModel.Winner?.ToPlayerDto(),
            CreatedAt = setModel.CreatedAt,
            Points = setModel.Points.Select(p => p.ToPointDto()).OrderByDescending(p => p.CreatedAt).ToList(),
        };
    }

    public static SetSummaryDto ToSetSummaryDto(this Set setModel)
    {
        var players = setModel.Game.PlayerGames.Select(pg => pg.Player).ToList();
        return new SetSummaryDto
        {
            Id = setModel.Id,
            Winner = $"{setModel.Winner?.FirstName} {setModel.Winner?.LastName}",
            CreatedAt = setModel.CreatedAt,
            Player1 = setModel.Points.ToPlayerSetPointDto(players[0]),
            Player2 = setModel.Points.ToPlayerSetPointDto(players[1]),
        };
    }

    public static PlayerSetPointDto ToPlayerSetPointDto(this ICollection<Point> points, Player player)
    {
        return new PlayerSetPointDto
        {
            FullName = $"{player.FirstName} {player.LastName}",
            Points = points.Where(p => p.Winner.Id == player.Id).Count()
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