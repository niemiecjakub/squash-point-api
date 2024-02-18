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
    Task<PlayerLeague> AddPlayerToLeagueAsync(int leagueId, int playerId);
    Task<bool> IsPlayerInLeagueAsync(int leagueId, int playerId);
    Task<PlayerLeague?> DeleteAsync(int leagueId, int playerId);
} 