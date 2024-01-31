using SquashPointAPI.Data;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

internal class LeagueRepository(DataContext context) : ILeagueRepository

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

    public bool CreateLeague(League league)
    {
        context.Add(league);
        return Save();
    }

    public bool AddPlayerToLeague(int leagueId, int playerId)
    {
        var league = context.Leagues.Where(l => l.Id == leagueId).FirstOrDefault();
        var player = context.Players.Where(p => p.Id == playerId).FirstOrDefault();
        var playerLeague = new PlayerLeague()
        {
            Player = player,
            League = league
        };
        context.Add(playerLeague);
        return Save();
    }

    public bool IsPlayerInLeague(int leagueId, int playerId)
    {
        return context.PlayerLeagues.Any(pl => pl.LeagueId == leagueId && pl.PlayerId == playerId);
    }

    public bool Save()
    {
        var saved = context.SaveChanges();
        return saved > 0 ? true : false;
    }
}