using SquashPointAPI.Dto.Game;
using SquashPointAPI.Models;

namespace SquashPointAPI.Mappers;

public static class GameMapper
{
    public static GameDto ToGameDto(this Game gameModel)
    {
        return new GameDto
        {
            Id = gameModel.Id,
            Date = gameModel.Date.ToString("dddd, dd MMMM yyyy"),
            Status = gameModel.Status,
            Winner = gameModel.Winner == null ? null : $"{gameModel.Winner?.FirstName} {gameModel.Winner?.LastName}",
            CreatedAt = gameModel.CreatedAt,
            League = gameModel.League.Name,
            Players = gameModel.PlayerGames.Select(pg => pg.Player.ToPlayerDto()).ToList()
        };
    }

    public static FinishedGameDto ToFinishedGameDto(this Game gameModel)
    {
        var players = gameModel.PlayerGames.Select(pg => pg.Player).ToList();
        var sets = gameModel.Sets.ToList();

        return new FinishedGameDto
        {
            Id = gameModel.Id,
            Status = gameModel.Status,
            Winner = $"{gameModel.Winner?.FirstName} {gameModel.Winner?.LastName}",
            League = gameModel.League.Name,
            Players = new[]
            {
                new PlayerGameSummary()
                {
                    FullName = $"{players[0].FirstName} {players[0].LastName}",
                    Sets = sets.Count(s => s.Winner?.Id == players[0].Id),
                    Photo = players[0].Photo?.ImageData
                },
                new PlayerGameSummary()
                {
                    FullName = $"{players[1].FirstName} {players[1].LastName}",
                    Sets = sets.Count(s => s.Winner?.Id == players[1].Id),
                    Photo = players[1].Photo?.ImageData
                }
            }
        };
    }

    public static GameDetailsDto ToGameDetailsDto(this Game gameModel)
    {
        var players = gameModel.PlayerGames.Select(pg => pg.Player).ToList();
        return new GameDetailsDto
        {
            Id = gameModel.Id,
            League = gameModel.League.Name,
            Status = gameModel.Status,
            Date = gameModel.Date.ToString("dddd, dd MMMM yyyy"),
            Winner = gameModel.Winner == null ? null : $"{gameModel.Winner?.FirstName} {gameModel.Winner?.LastName}",
            Players = players.Select(p => p.ToPlayerDto()).ToList(),
            Sets = gameModel.Sets.Select(s => s.ToSetDto()).OrderByDescending(s => s.CreatedAt).ToList(),
            Player1Sets = gameModel.Sets.Count(s => s.Winner == players[0]),
            Player2Sets = gameModel.Sets.Count(s => s.Winner == players[1]),
            CreatedAt = gameModel.CreatedAt
        };
    }

    public static LeagueGamesDto ToLeagueGamesDto(this ICollection<Game> games)
    {
        return new LeagueGamesDto
        {
            FinishedGames = games.Where(g => g.Status == "Finished").OrderByDescending(g => g.Date)
                .Select(g => g.ToFinishedGameDto()).ToList(),
            UpcommingGames = games.Where(g => g.Status == "Unfinished").OrderByDescending(g => g.Date)
                .Select(g => g.ToGameDto()).ToList(),
            LiveGames = games.Where(g => g.Status == "Started").OrderByDescending(g => g.Date)
                .Select(g => g.ToGameDto()).ToList(),
        };
    }

    public static PlayerGamesDto ToLPlayerGamesDto(this ICollection<Game> games)
    {
        return new PlayerGamesDto
        {
            LastGames = games.Where(g => g.Status == "Finished").OrderByDescending(g => g.Date).Take(5)
                .Select(g => g.ToFinishedGameDto()).ToList(),
            NextGames = games.Where(g => g.Status == "Unfinished").OrderByDescending(g => g.Date).Take(5)
                .Select(g => g.ToGameDto()).ToList(),
        };
    }

    public static Game ToGameFromCreateDto(this CreateGameDto createGameDto, League league)
    {
        return new Game
        {
            League = league,
            Status = "Unfinished",
            CreatedAt = DateTime.Now,
            Date = createGameDto.Date
        };
    }
}