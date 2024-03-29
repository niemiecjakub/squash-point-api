﻿using SquashPointAPI.Dto.Game;
using SquashPointAPI.Models;

namespace SquashPointAPI.Mappers;

public static class GameMapper
{
    public static GameDto ToGameDto(this Game gameModel)
    {
        return new GameDto
        {
            Id = gameModel.Id,
            Date = gameModel.Date,
            Status = gameModel.Status,
            Winner = gameModel.Winner?.ToPlayerDto(),
            CreatedAt = gameModel.CreatedAt,
            League = gameModel.League?.Name,
            Players = gameModel.PlayerGames.Select(pg => pg.Player.ToPlayerDto()).ToList()
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
            Date = gameModel.Date,
            Winner = gameModel.Winner?.ToPlayerDto(),
            Players = players.Select(p => p.ToPlayerDto()).ToList(),
            Sets = gameModel.Sets.Select(s => s.ToSetDto()).OrderByDescending(s => s.CreatedAt).ToList(),
            Player1Sets = gameModel.Sets.Count(s => s.Winner == players[0]),
            Player2Sets = gameModel.Sets.Count(s => s.Winner == players[1]),
            CreatedAt = gameModel.CreatedAt
        };
    }

    public static GameSummaryDto ToGameSummaryDto(this Game gameModel)
    {
        return new GameSummaryDto
        {
            Id = gameModel.Id,
            League = gameModel.League.Name,
            Status = gameModel.Status,
            Winner = $"{gameModel.Winner.FirstName} {gameModel.Winner.LastName}",
            CreatedAt = gameModel.CreatedAt,
            Sets = gameModel.Sets.Select(set => set.ToSetSummaryDto()).OrderBy(s => s.CreatedAt).ToList()
        };
    }
}