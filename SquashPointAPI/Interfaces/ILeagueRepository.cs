using SquashPointAPI.Dto.League;
using SquashPointAPI.Helpers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface ILeagueRepository
{
    Task<ICollection<League>> GetLeaguesAsync();
    Task<League> GetLeagueByIdAsync(int leagueId);
    Task<ICollection<Player>> GetLeaguePlayersAsync(int leagueId);
    Task<ICollection<Game>> GetLeagueGamesAsync(int leagueId, GameQueryObject gameQuery);
    Task<bool> LeagueExistsAsync(int leagueId);
    Task<League> CreateLeagueAsync(League league);
    Task<PlayerLeague> AddPlayerToLeagueAsync(PlayerLeague playerLeague);
    Task<bool> IsPlayerInLeagueAsync(int leagueId, string playerId);
    Task<PlayerLeague?> RemovePlayerAsync(int leagueId, string playerId);
    Task<League?> DeleteAsync(int leagueId);
    Task<League> UpdateLeague(int leagueId, UpdateLeagueDto updateLeagueDto);
    Task<League> UpdateLeaguePhoto(int leagueId, Image image);
}