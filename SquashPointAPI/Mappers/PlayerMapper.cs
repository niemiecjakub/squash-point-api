using SquashPointAPI.Dto;
using SquashPointAPI.Dto.League;
using SquashPointAPI.Dto.Player;
using SquashPointAPI.Models;

namespace SquashPointAPI.Mappers;

public static class PlayerMapper
{
    public static PlayerDto ToPlayerDto(this Player playerModel)
    {
        return new PlayerDto
        {
            Id = playerModel.Id,
            FirstName = playerModel.FirstName,
            LastName = playerModel.LastName,
            FullName = playerModel.FirstName + " " + playerModel.LastName,
            Email = playerModel.Email,
            Sex = playerModel.Sex,
            CreatedAt = playerModel.CreatedAt,
        };
    }

    public static Player ToPlayerFromCreateDto(this CreatePlayerDto playerDto)
    {
        return new Player
        {
            FirstName = playerDto.FirstName,
            LastName = playerDto.LastName,
            Email = playerDto.Email,
            Sex = playerDto.Sex,
        };
    }

    public static PlayerDetailsDto ToPlayerDetailsDto(this Player playerModel)
    {
        return new PlayerDetailsDto
        {
            Id = playerModel.Id,
            FirstName = playerModel.FirstName,
            LastName = playerModel.LastName,
            FullName = playerModel.FirstName + " " + playerModel.LastName,
            Sex = playerModel.Sex,
            Email = playerModel.Email,
            Leagues = playerModel.PlayerLeagues.Select(pl => pl.League.ToLeagueDto()).ToList(),
            Games = playerModel.PlayerGames.Select(pg => pg.Game.ToGameDto()).ToList()
        };
    }

    
    public static LeaguePlayerDto ToLeaguePlayerDto(this Player playerModel)
    {

        return new LeaguePlayerDto()
        {
            Id = playerModel.Id,
            FirstName = playerModel.FirstName,
            LastName = playerModel.LastName,
            FullName = playerModel.FirstName + " " + playerModel.LastName,
            Sex = playerModel.Sex,
            Email = playerModel.Email,
            GamesPlayed = playerModel.PlayerGames.ToList().Count,
            Score = playerModel.PlayerLeagues.First().Score,
        };
    }
}