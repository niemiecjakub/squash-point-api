using SquashPointAPI.Dto;
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
            Date = gameModel.Date,
            Status = gameModel.Status,
            Winner = gameModel.Winner?.ToPlayerDto(),
            CreatedAt = gameModel.CreatedAt,
        };
    }
    
    public static GameDetailsDto ToGameDetailsDto(this Game gameModel)
    {
        return new GameDetailsDto
        {
            Id = gameModel.Id,
            CreatedAt = gameModel.CreatedAt,
            Status = gameModel.Status,
            Date = gameModel.Date,
            Winner = gameModel.Winner?.ToPlayerDto(),
            Players = gameModel.PlayerGames.Select(pg => pg.Player.ToPlayerDto()).ToList()
        };
    }
}