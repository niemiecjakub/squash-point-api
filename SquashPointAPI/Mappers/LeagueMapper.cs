using SquashPointAPI.Dto;
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
        };
    }

    public static LeagueDetailsDto ToLeagueDetailsDto(this League leagueModel)
    {
        return new LeagueDetailsDto
        {
            Id = leagueModel.Id,
            Name = leagueModel.Name,
            Players = leagueModel.PlayerLeagues.Select(pl => pl.Player.ToLeaguePlayerDto()).OrderByDescending(p => p.Score).ToList(),
            Games = leagueModel.Games.Select(g => g.ToGameDto()).OrderByDescending(g => g.Date).ToList(),
        };
    }
    
    public static League ToLeagueFromCreateDTO(this CreateLeagueDto leagueDto)
    {
        return new League
        {
            Name = leagueDto.Name,
        };
    }
}