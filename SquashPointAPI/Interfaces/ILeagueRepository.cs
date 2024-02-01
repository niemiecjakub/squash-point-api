using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface ILeagueRepository
{
    Task<ICollection<League>> GetAllLeaguesAsync();
    Task<League> GetLeagueByIdAsync(int leagueId);
    Task<ICollection<Player>> GetAllLeaguePlayersAsync(int leagueId);
    Task<ICollection<Game>> GetAllLeagueGamesAsync(int leagueId);
    Task<bool> LeagueExistsAsync(int leagueId);
    Task<League> CreateLeagueAsync(League league);
    Task<PlayerLeague> AddPlayerToLeagueAsync(int leagueId, int playerId);
    Task<bool> IsPlayerInLeagueAsync(int leagueId, int playerId);
} 