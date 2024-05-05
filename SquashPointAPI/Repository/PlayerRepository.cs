using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Data;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class PlayerRepository(ApplicationDBContext context, UserManager<Player> userManager) : IPlayerRepository
{
    public async Task<ICollection<Player>> GetPlayersAsync()
    {
        return await context.Players.Include(p => p.Photo).OrderBy(p => p.Id).ToListAsync();
    }

    public async Task<Player> GetPlayerAsync(string playerId)
    {
        return await context.Players
            .Include(p => p.Photo)
            .Include(p => p.Following)
            .FirstAsync(p => p.Id.Equals(playerId));

        // return await context.Players
        //     .Include(p => p.Photo)
        //     .Include(p => p.Following)
        //     .Include(p => p.PlayerLeagues)
        //     .ThenInclude(pg => pg.League)
        //     .Include(p => p.PlayerGames)
        //     .ThenInclude(pg => pg.Game)
        //     .ThenInclude(g => g.PlayerGames)
        //     .ThenInclude(pg => pg.Player)
        //     .ThenInclude(p => p.Photo)
        //     .Include(p => p.PlayerGames)
        //     .ThenInclude(pg => pg.Game)
        //     .ThenInclude(pg => pg.League)
        //     .Include(p => p.PlayerGames)
        //     .ThenInclude(pg => pg.Game)
        //     .ThenInclude(g => g.Sets)
        //     .FirstAsync(p => p.Id.Equals(playerId));
    }

    public async Task<ICollection<Game>> GetPlayerGamesAsync(string playerId)
    {
        return await context.Games
            .Where(g => g.PlayerGames.Any(pg => pg.PlayerId.Equals(playerId)))
            .Include(g => g.League)
            .Include(g => g.PlayerGames)
            .ThenInclude(pg => pg.Player)
            .ThenInclude(p => p.Photo)
            .Include(g => g.Sets)
            .Include(g => g.Winner)
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
        return await context.Leagues
            .Where(l => l.PlayerLeagues.Any(pl => pl.PlayerId == playerId))
            .Include(l => l.PlayerLeagues)
            .Include(l => l.Owner)
            .ToListAsync();
    }

    public async Task<ICollection<PlayerFriend>> GetPlayerFriendsAsync(string playerId)
    {
        return await context.PlayerFriends
            .Where(pf => pf.PlayerId.Equals(playerId) || pf.FriendId.Equals(playerId))
            .Include(pf => pf.Friend)
            .Include(pf => pf.Player)
            .ThenInclude(p => p.Photo)
            .ToListAsync();
    }

    public async Task<ICollection<Player>> GetPlayerFollowersAsync(string playerId)
    {
        return await context.FollowerFollowee
            .Where(pf => pf.Followee.Id.Equals(playerId))
            .Include(pf => pf.Follower)
            .ThenInclude(f => f.Photo)
            .Select(pf => pf.Follower)
            .ToListAsync();
    }

    public async Task<ICollection<Player>> GetPlayerFolloweesAsync(string playerId)
    {
        return await context.FollowerFollowee
            .Where(pf => pf.Follower.Id.Equals(playerId))
            .Include(pf => pf.Followee)
            .ThenInclude(f => f.Photo)
            .Select(pf => pf.Followee)
            .ToListAsync();
    }


    public async Task<bool> PlayerExistsAsync(string playerId)
    {
        return await context.Players.AnyAsync(p => p.Id.Equals(playerId));
    }
}