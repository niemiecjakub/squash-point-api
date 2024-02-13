using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Data;
using SquashPointAPI.Dto.Player;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

internal class LeagueRepository(DataContext context) : ILeagueRepository

{
    public async Task<ICollection<League>> GetAllLeaguesAsync()
    {
        return await context.Leagues.OrderBy(l => l.Id).ToListAsync();
    }

    public async Task<League> GetLeagueByIdAsync(int leagueId)
    {
        return await context.Leagues
            .Include(l => l.PlayerLeagues)
            .ThenInclude(l => l.Player)
            .Include(l => l.Games)
            .ThenInclude(g => g.PlayerGames)
            .FirstOrDefaultAsync(l => l.Id == leagueId);
    }

    public async Task<ICollection<Player>> GetAllLeaguePlayersAsync(int leagueId)
    {
        // return await context.PlayerLeagues
        //     .Where(pl => pl.League.Id == leagueId)
        //     .Select(pl => pl.Player)
        //     .ToListAsync();


        // return await context.Players
        //     .Include(p => p.PlayerLeagues)
        //     .ThenInclude(pl => pl.League)
        //     .Include(p => p.PlayerGames)
        //     .ThenInclude(pg => pg.Game)
        //     .Where(p => p.PlayerLeagues.Any(pl => pl.LeagueId == leagueId))
        //     .Where(p => p.PlayerGames.Any(pg => pg.Game.League.Id == leagueId))
        //     .ToListAsync();
        
        return await context.Players
            .Include(p => p.PlayerLeagues)
            .ThenInclude(pl => pl.League)
            .Include(p => p.PlayerGames)
            .ThenInclude(pg => pg.Game)
            .Where(p => p.PlayerLeagues.Any(pl => pl.LeagueId == leagueId))
            .ToListAsync();
    }

    public async Task<ICollection<Game>> GetAllLeagueGamesAsync(int leagueId)
    {
        return await context.Games.Where(g => g.League.Id == leagueId).ToListAsync();
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
        var league = await context.Leagues.FirstOrDefaultAsync(l => l.Id == leagueId);
        var player = await context.Players.FirstOrDefaultAsync(p => p.Id == playerId);
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