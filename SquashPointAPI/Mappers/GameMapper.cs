using SquashPointAPI.Dto;
using SquashPointAPI.Dto.Game;
using SquashPointAPI.Dto.Player;
using SquashPointAPI.Models;

namespace SquashPointAPI.Mappers;

public static class GameMapper
{
    public static GameDto ToGameDto(this Game gameModel)
    {
        var dto = new GameDto();
        dto.Id = gameModel.Id;
        dto.Date = gameModel.Date;
        dto.Status = gameModel.Status;
        dto.Winner = gameModel.Winner?.ToPlayerDto();
        dto.CreatedAt = gameModel.CreatedAt;
        dto.LeagueId = gameModel.League?.Id;
        dto.Players = gameModel.PlayerGames.Select(pg => pg.Player.ToPlayerDto()).ToList();

        return dto;
        // return new GameDto
        // {
        //     Id = gameModel.Id,
        //     Date = gameModel.Date,
        //     Status = gameModel.Status,
        //     Winner = gameModel.Winner?.ToPlayerDto(),
        //     CreatedAt = gameModel.CreatedAt,
        //     LeagueId = gameModel.League.Id,
        //     Players = gameModel.PlayerGames.Select(pg => pg.Player.ToPlayerDto()).ToList()
        // };
    }
    
    public static GameDetailsDto ToGameDetailsDto(this Game gameModel)
    {
        var players = gameModel.PlayerGames.Select(pg => pg.Player).ToList();
        // var points = gameModel.Sets.SelectMany(s => s.Points).ToList();
        return new GameDetailsDto
        {
            Id = gameModel.Id,
            League = gameModel.League.ToLeagueDto(),
            Status = gameModel.Status,
            Date = gameModel.Date,
            Winner = gameModel.Winner?.ToPlayerDto(),
            Players = players.Select(p => p.ToPlayerDto()).ToList(),
            Sets = gameModel.Sets.Select(s => s.ToSetDto()).OrderByDescending(s => s.CreatedAt).ToList(),
            Player1Sets = gameModel.Sets.Count(s => s.Winner == players[0]),
            Player2Sets = gameModel.Sets.Count(s => s.Winner == players[1]),
            CreatedAt = gameModel.CreatedAt,
        };
    }
}
