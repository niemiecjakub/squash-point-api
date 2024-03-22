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
        return await context.Players.OrderBy(p => p.Id).ToListAsync();
    }

    public async Task<Player> GetPlayerAsync(string playerId)
    {
        return await context.Players
            .Include(p => p.Following)
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

    public async Task<ICollection<PlayerFriend>> GetPlayerFriendsAsync(string playerId)
    {
        return await context.PlayerFriends
            .Where(pf => pf.PlayerId.Equals(playerId))
            .Include(pf => pf.Friend)
            .ToListAsync();
    }

    public async Task<ICollection<Player>> GetPlayerFollowersAsync(string playerId)
    {
        return await context.FollowerFollowee.Where(pf => pf.Followee.Id == playerId).Select(pf => pf.Follower)
            .ToListAsync();
    }

    public async Task<ICollection<Player>> GetPlayerFolloweesAsync(string playerId)
    {
        return await context.FollowerFollowee.Where(pf => pf.Follower.Id == playerId).Select(pf => pf.Followee)
            .ToListAsync();
    }

    public async Task<bool> FollowPlayerAsync(FollowerFollowee playerFollow)
    {
        await context.FollowerFollowee.AddAsync(playerFollow);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UnollowPlayerAsync(Player follower, Player followee)
    {
        var followerFollowee =
            await context.FollowerFollowee.FirstOrDefaultAsync(ff =>
                ff.Followee == followee && ff.Follower == follower);
        if (followerFollowee == null) return false;
        context.FollowerFollowee.Remove(followerFollowee);
        await context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> PlayerExistsAsync(string playerId)
    {
        return await context.Players.AnyAsync(p => p.Id.Equals(playerId));
    }

    public async Task<Player> LoginUserAsync(string email)
    {
        return await userManager.Users.Include(p => p.Following).FirstAsync(u => u.Email.ToLower() == email.ToLower());
    }

    public async Task<bool> FriendRequestAsync(PlayerFriend playerFriend)
    {
        await context.PlayerFriends.AddAsync(playerFriend);
        await context.SaveChangesAsync();
        return true;
    }
}