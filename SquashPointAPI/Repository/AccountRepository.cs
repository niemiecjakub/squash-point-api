using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Data;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class AccountRepository(ApplicationDBContext context, UserManager<Player> userManager) : IAccountRepository
{
    public async Task<Player> LoginUserAsync(string email)
    {
        return await userManager.Users
            .Include(p => p.Following)
            .Include(p => p.Photo)
            .FirstAsync(u => u.Email.ToLower().Equals(email.ToLower()));
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

    public async Task<bool> SendFriendRequestAsync(PlayerFriend playerFriend)
    {
        await context.PlayerFriends.AddAsync(playerFriend);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AcceptFriendRequestAsync(Player sender, Player receiver, int status)
    {
        var playerFriend =
            await context.PlayerFriends.FirstAsync(pf =>
                pf.Player == sender && pf.Friend == receiver && pf.Status == 0);
        playerFriend.Status = status;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteFriendAsync(Player player, Player friend)
    {
        var playerFriend =
            await context.PlayerFriends.FirstAsync(pf =>
                pf.Player == player || pf.Friend == player && pf.Player == friend || pf.Friend == friend);
        context.PlayerFriends.Remove(playerFriend);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Player> UpdatePlayerPhoto(string playerId, Image image)
    {
        var player = await context.Players.FirstOrDefaultAsync(p => p.Id == playerId);
        player.Photo = image;
        await context.SaveChangesAsync();
        return player;
    }
}