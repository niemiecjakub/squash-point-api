using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface ILeagueRepository
{
    ICollection<League> GetAllLeagues();
    League GetLeagueById(int leagueId);
    League GetLeagueByName(string leagueName);
    ICollection<Player> GetAllLeaguePlayers(int leagueId);
    bool LeagueExists(int leagueId);
    bool CreateLeague(League league);
    bool AddPlayerToLeague(int leagueId, int playerId);
    bool IsPlayerInLeague(int leagueId, int playerId);
    bool Save();
} 