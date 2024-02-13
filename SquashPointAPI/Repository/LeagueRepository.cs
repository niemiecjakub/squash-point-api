using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Data;
using SquashPointAPI.Dto.Player;
using SquashPointAPI.Helpers;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

internal class LeagueRepository(DataContext context) : ILeagueRepository

{
    public async Task<ICollection<League>> GetLeaguesAsync()
    {
        return await context.Leagues.OrderBy(l => l.Id).ToListAsync();
    }

    public async Task<League> GetLeagueByIdAsync(int leagueId)
    {
        return await context.Leagues
            .Include(l => l.Games.Where(g => g.League.Id == leagueId))
            .Include(p => p.PlayerLeagues.Where(pl => pl.LeagueId == leagueId))
            .ThenInclude(pl => pl.Player)
            .ThenInclude(p => p.PlayerGames.Where(pg => pg.Game.League.Id == leagueId))
            .FirstAsync(l => l.Id == leagueId);
    }

    public async Task<ICollection<Player>> GetLeaguePlayersAsync(int leagueId)
    {
        return await context.Players
            .Where(p => p.PlayerLeagues.Any(pl => pl.LeagueId == leagueId))
            .Include(p => p.PlayerGames.Where(pg => pg.Game.League.Id == leagueId))
            .ThenInclude(pg => pg.Game)
            .Include(p => p.PlayerLeagues.Where(pl => pl.LeagueId == leagueId))
            .ToListAsync();
    }

    public async Task<ICollection<Game>> GetLeagueGamesAsync(int leagueId, QueryObject query)
    {
        var games = context.Games
            .Where(g => g.League.Id == leagueId)
            .Include(g => g.League)
            .Include(g => g.PlayerGames)
            .ThenInclude(pg => pg.Player)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.GameStatus))
        {
            games = games.Where(g => g.Status.Equals(query.GameStatus));
        }
        
        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        return await games.OrderByDescending(g => g.Date).Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<bool> LeagueExistsAsync(int leagueId)
    {
        return await context.Leagues.AnyAsync(l => l.Id == leagueId);
    }

    public async Task<League> CreateLeagueAsync(League league)
    {
        await context.AddAsync(league);
        await context.SaveChangesAsync();
        return league;
    }

    public async Task<PlayerLeague> AddPlayerToLeagueAsync(int leagueId, int playerId)
    {
        var league = await context.Leagues.FirstAsync(l => l.Id == leagueId);
        var player = await context.Players.FirstAsync(p => p.Id == playerId);
        var playerLeague = new PlayerLeague()
        {
            Player = player,
            League = league,
            Score = 0
        };
        await context.AddAsync(playerLeague);
        await context.SaveChangesAsync();
        return playerLeague;
    }

    public async Task<bool> IsPlayerInLeagueAsync(int leagueId, int playerId)
    {
        return await context.PlayerLeagues.AnyAsync(pl => pl.LeagueId == leagueId && pl.PlayerId == playerId);
    }
}