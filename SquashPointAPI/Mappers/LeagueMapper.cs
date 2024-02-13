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
            CreatedAt = leagueModel.CreatedAt,
        };
    }

    public static League ToLeagueFromCreateDTO(this CreateLeagueDto leagueDto)
    {
        return new League
        {
            Name = leagueDto.Name,
        };
    }

    public static LeagueDetailsDto ToLeagueDetailsDto(this League leagueModel, int leagueId)
    {
        return new LeagueDetailsDto
        {
            Id = leagueModel.Id,
            Name = leagueModel.Name,
            Players = leagueModel.PlayerLeagues.Select(pl => pl.Player.ToLeaguePlayerDto(leagueId)).ToList(),
            Games = leagueModel.Games.Select(g => g.ToGameDetailsDto()).ToList(),
        };
    }
}