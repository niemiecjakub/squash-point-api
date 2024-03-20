using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Data;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class PlayerRepository(ApplicationDBContext context) : IPlayerRepository
{
    public async Task<ICollection<Player>> GetPlayersAsync()
    {
        return await context.Players.OrderBy(p => p.Id).ToListAsync();
    }

    public async Task<Player> GetPlayerAsync(string playerId)
    {
        return await context.Players
            .Include(p => p.PlayerLeagues)
            .ThenInclude(pg => pg.League)
            .Include(p => p.PlayerGames)
            .ThenInclude(pg => pg.Game)
            .ThenInclude(g => g.PlayerGames)
            .ThenInclude(pg => pg.Player)
            .Include(p => p.PlayerGames)
            .ThenInclude(pg => pg.Game)
            .ThenInclude(pg => pg.League)
            .FirstOrDefaultAsync(p => p.Id.Equals(playerId));
    }

    public async Task<ICollection<Game>> GetPlayerGamesAsync(string playerId)
    {
        return await context.Games
            .Include(g => g.League)
            .Include(g => g.PlayerGames)
            .ThenInclude(pg => pg.Player)
            .Where(g => g.PlayerGames.Any(pg => pg.PlayerId.Equals(playerId)))
            .ToListAsync();
    }

    public async Task<ICollection<Game>> GetPlayerFinishedGamesAsync(string playerId)
    {
        return await context.Games
            .Where(g => g.PlayerGames.Any(pg => pg.PlayerId.Equals(playerId)) && g.Status == "Finished")
            .Include(g => g.Sets)
            .ThenInclude(s => s.Winner)
            .Include(g => g.Sets)
            .ThenInclude(s => s.Points)
            .ToListAsync();
    }

    public async Task<ICollection<League>> GetPlayerLeagues(string playerId)
    {
        return await context.PlayerLeagues
            .Where(pl => pl.PlayerId.Equals(playerId))
            .Select(pl => pl.League)
            .ToListAsync();
    }

    public async Task<ICollection<Player>> GetPlayerFriendsAsync(string playerId)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Player>> GetPlayerFollowersAsync(string playerId)
    {
        return await context.FollowerFollowee.Where(pf => pf.Followee.Id == playerId).Select(pf => pf.Followee)
            .ToListAsync();
    }

    public async Task<ICollection<Player>> GetPlayerFollowingAsync(string playerId)
    {
        return await context.FollowerFollowee.Where(pf => pf.FollowerId == playerId).Select(pf => pf.Followee)
            .ToListAsync();
    }

    public async Task FollowPlayerAsync(FollowerFollowee playerFollow)
    {
        await context.FollowerFollowee.AddAsync(playerFollow);
    }


    public async Task<bool> PlayerExistsAsync(string playerId)
    {
        return await context.Players.AnyAsync(p => p.Id.Equals(playerId));
    }
}