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
}