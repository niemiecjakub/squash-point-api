using SquashPointAPI.Dto.Account;
using SquashPointAPI.Dto.Player;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Mappers;

public static class PlayerMapper
{
    public static PlayerDto ToPlayerDto(this Player playerModel)
    {
        return new PlayerDto
        {
            Id = playerModel.Id,
            FullName = playerModel.FirstName + " " + playerModel.LastName
        };
    }


    public static PlayerDetailsDto ToPlayerDetailsDto(this Player playerModel)
    {
        return new PlayerDetailsDto
        {
            Id = playerModel.Id,
            FullName = playerModel.FirstName + " " + playerModel.LastName,
            Sex = playerModel.Sex,
            Email = playerModel.Email,
            Leagues = playerModel.PlayerLeagues.Select(pl => pl.League.ToLeagueDto()).ToList(),
            Games = playerModel.PlayerGames.Select(pg => pg.Game.ToGameDto()).ToList()
        };
    }


    public static LeaguePlayerDto ToLeaguePlayerDto(this Player playerModel)
    {
        return new LeaguePlayerDto
        {
            Id = playerModel.Id,
            FullName = playerModel.FirstName + " " + playerModel.LastName,
            Score = playerModel.PlayerLeagues.First().Score,
            GamesPlayed = playerModel.PlayerGames.Count(pg => pg.Game.Status == "Finished"),
            GamesWon = playerModel.PlayerGames.Count(pg => pg.Game.Winner == playerModel),
            GamesLost = playerModel.PlayerGames.Where(pg => pg.Game.Status == "Finished")
                .Count(pg => pg.Game.Winner != playerModel)
        };
    }

    public static NewUserDto ToNewUserDto(this Player playerModel, string token)
    {
        return new NewUserDto
        {
            Id = playerModel.Id,
            FullName = playerModel.FirstName + " " + playerModel.LastName,
            Email = playerModel.Email,
            Token = token
        };
    }
}