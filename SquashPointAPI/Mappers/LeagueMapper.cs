using SquashPointAPI.Dto.League;
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
        };
    }

    public static LeagueDetailsDto ToLeagueDetailsDto(this League leagueModel)
    {
        return new LeagueDetailsDto
        {
            Id = leagueModel.Id,
            Name = leagueModel.Name,
            Owner = $"{leagueModel.Owner.FirstName} {leagueModel.Owner.LastName}",
            PlayerCount = leagueModel.PlayerLeagues.Select(pl => pl.Player.ToLeaguePlayerDto()).Count(),
            Players = leagueModel.PlayerLeagues.Select(pl => pl.Player.ToLeaguePlayerDto())
                .OrderByDescending(p => p.Score).ToList(),
            Games = leagueModel.Games.Select(g => g.ToGameDto()).OrderByDescending(g => g.Date).ToList()
        };
    }

    public static League ToLeagueFromCreateDTO(this CreateLeagueDto leagueDto, Player owner)
    {
        return new League
        {
            Name = leagueDto.Name,
            Owner = owner
        };
    }
}