using SquashPointAPI.Data;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class LeagueRepository(DataContext context) : ILeagueRepository

{
    public ICollection<League> GetAllLeagues()
    {
        return context.Leagues.OrderBy(l => l.Id).ToList();
    }

    public League GetLeagueById(int leagueId)
    {
        return context.Leagues.Where(l => l.Id == leagueId).FirstOrDefault();
    }

    public League GetLeagueByName(string leagueName)
    {
        return context.Leagues.Where(l => l.Name == leagueName).FirstOrDefault();
    }

    public ICollection<Player> GetAllLeaguePlayers(int leagueId)
    {
        return context.PlayerLeagues.Where(pl => pl.League.Id == leagueId).Select(pl => pl.Player).ToList();
    }

    public bool LeagueExists(int leagueId)
    {
        return context.Leagues.Any(l => l.Id == leagueId);
    }
}