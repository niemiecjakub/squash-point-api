using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Data;
using SquashPointAPI.Dto.League;
using SquashPointAPI.Helpers;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class LeagueRepository(ApplicationDBContext context) : ILeagueRepository
{
    public async Task<ICollection<League>> GetLeaguesAsync()
    {
        return await context.Leagues
            .Include(l => l.Owner)
            .Include(p => p.PlayerLeagues)
            .OrderBy(l => l.Id)
            .ToListAsync();
    }

    public async Task<League> GetLeagueByIdAsync(int leagueId)
    {
        return await context.Leagues
            .Include(l => l.Photo)
            .Include(l => l.Owner)
            .Include(p => p.PlayerLeagues)
            .FirstOrDefaultAsync(l => l.Id == leagueId);
    }


    public async Task<ICollection<Player>> GetLeaguePlayersAsync(int leagueId)
    {
        return await context.Players
            .Where(p => p.PlayerLeagues.Any(pl => pl.LeagueId == leagueId))
            .Include(p => p.Photo)
            .Include(p => p.PlayerGames.Where(pg => pg.Game.League.Id == leagueId))
            .ThenInclude(pg => pg.Game)
            .Include(p => p.PlayerLeagues.Where(pl => pl.LeagueId == leagueId))
            .ToListAsync();
    }

    public async Task<ICollection<Game>> GetLeagueGamesAsync(int leagueId, GameQueryObject gameQuery)
    {
        var games = context.Games
            .Where(g => g.League.Id == leagueId)
            .Include(g => g.League)
            .Include(g => g.PlayerGames)
            .ThenInclude(pg => pg.Player)
            .ThenInclude(p => p.Photo)
            .Include(g => g.Sets)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(gameQuery.GameStatus))
            games = games.Where(g => g.Status.Equals(gameQuery.GameStatus));

        var skipNumber = (gameQuery.PageNumber - 1) * gameQuery.PageSize;
        return await games.OrderByDescending(g => g.Date).Skip(skipNumber).Take(gameQuery.PageSize).ToListAsync();
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

    public async Task<PlayerLeague> AddPlayerToLeagueAsync(PlayerLeague playerLeague)
    {
        await context.AddAsync(playerLeague);
        await context.SaveChangesAsync();
        return playerLeague;
    }

    public async Task<bool> IsPlayerInLeagueAsync(int leagueId, string playerId)
    {
        return await context.PlayerLeagues.AnyAsync(pl => pl.LeagueId == leagueId && pl.PlayerId.Equals(playerId));
    }

    public async Task<PlayerLeague?> RemovePlayerAsync(int leagueId, string playerId)
    {
        var playerLeague = await context.PlayerLeagues
            .Where(pl => pl.LeagueId == leagueId)
            .FirstOrDefaultAsync(pl => pl.PlayerId.Equals(playerId));

        if (playerLeague == null) return null;

        context.PlayerLeagues.Remove(playerLeague);
        await context.SaveChangesAsync();
        return playerLeague;
    }

    public async Task<League?> DeleteAsync(int leagueId)
    {
        var league = await context.Leagues.FirstOrDefaultAsync(l => l.Id == leagueId);

        if (league == null) return null;

        context.Leagues.Remove(league);
        await context.SaveChangesAsync();
        return league;
    }

    public async Task<League> UpdateLeague(int leagueId, UpdateLeagueDto updateLeagueDto)
    {
        var league = await context.Leagues.FirstOrDefaultAsync(l => l.Id == leagueId);
        league.Name = updateLeagueDto.Name;
        league.Description = updateLeagueDto.Description;
        league.Public = updateLeagueDto.Public;
        league.MaxPlayers = updateLeagueDto.MaxPlayers;

        await context.SaveChangesAsync();
        return league;
    }

    public async Task<League> UpdateLeaguePhoto(int leagueId, Image image)
    {
        var league = await context.Leagues.FirstOrDefaultAsync(l => l.Id == leagueId);
        league.Photo = image;
        await context.SaveChangesAsync();
        return league;
    }
}