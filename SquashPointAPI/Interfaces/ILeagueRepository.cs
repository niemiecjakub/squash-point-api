using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface ILeagueRepository
{
    public ICollection<League> GetAllLeagues();
    public League GetLeagueById(int leagueId);
    public League GetLeagueByName(string leagueName);
    public ICollection<Player> GetAllLeaguePlayers(int leagueId);
    public bool LeagueExists(int leagueId);
    public bool CreateLeague(League league);
    public bool AddPlayerToLeague(int leagueId, int playerId);
    public bool IsPlayerInLeague(int leagueId, int playerId);
    public bool Save();
} 