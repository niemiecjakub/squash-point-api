using SquashPointAPI.Dto.League;
using SquashPointAPI.Migrations;
using SquashPointAPI.Models;

namespace SquashPointAPI.Mappers;

public static class LeagueMapper
{
    public static LeagueDto ToLeagueDto(this League leagueModel)
    {
        return new LeagueDto
        {
            Id = leagueModel.Id,
            Name = leagueModel.Name,
            Owner = $"{leagueModel.Owner.FirstName} {leagueModel.Owner.LastName}",
            PlayerCount = leagueModel.PlayerLeagues.Select(pl => pl.Player.ToLeaguePlayerDto()).Count(),
            Description = leagueModel.Description,
            Public = leagueModel.Public,
            MaxPlayers = leagueModel.MaxPlayers
        };
    }

    public static LeagueDetailsDto ToLeagueDetailsDto(this League leagueModel)
    {
        return new LeagueDetailsDto
        {
            Id = leagueModel.Id,
            Name = leagueModel.Name,
            Description = leagueModel.Description,
            Public = leagueModel.Public,
            Owner = $"{leagueModel.Owner.FirstName} {leagueModel.Owner.LastName}",
            MaxPlayers = leagueModel.MaxPlayers,
            PlayerCount = leagueModel.PlayerLeagues.Select(pl => pl.Player.ToLeaguePlayerDto()).Count(),
            Players = leagueModel.PlayerLeagues.Select(pl => pl.Player.ToLeaguePlayerDto())
                .OrderByDescending(p => p.Score).ToList(),
            Games = leagueModel.Games.Select(g => g.ToGameDto()).OrderByDescending(g => g.Date).ToList(),
            Photo = leagueModel.Photo == null ? null : leagueModel.Photo.ImageData
        };
    }

    public static League ToLeagueFromCreateDTO(this CreateLeagueDto createLeagueDto, Player owner)
    {
        return new League
        {
            Name = createLeagueDto.Name,
            Owner = owner,
            Description = createLeagueDto.Description,
            MaxPlayers = createLeagueDto.MaxPlayers,
            Public = createLeagueDto.Public
        };
    }
}