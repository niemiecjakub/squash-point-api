using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface ILeagueRepository
{
    public ICollection<League> GetAllLeagues();
    public League GetLeagueById(int leagueId);
    public League GetLeagueByName(string leagueName);
    public ICollection<Player> GetAllLeaguePlayers(int leagueId);
    public bool LeagueExists(int leagueId);
}