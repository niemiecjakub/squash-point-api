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
            Description = leagueModel.Description,
            Public = leagueModel.Public,
            MaxPlayers = leagueModel.MaxPlayers,
            PlayerCount = leagueModel.PlayerLeagues.Count(),
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
            PlayerCount = leagueModel.PlayerLeagues.Count,
            Photo = leagueModel.Photo?.ImageData
        };
    }

    public static League ToLeagueFromCreateDto(this CreateLeagueDto createLeagueDto, Player owner)
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