using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Data;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class PlayerRepository(DataContext context) : IPlayerRepository
{
    public async Task<ICollection<Player>> GetPlayersAsync()
    {
        return await context.Players.OrderBy(p => p.Id).ToListAsync();
    }
    public async Task<Player> GetPlayerAsync(int playerId)
    {
        return await context.Players
            .FirstOrDefaultAsync(p => p.Id == playerId);
    }
    
    public async Task<ICollection<Game>> GetAllPlayerGamesAsync(int playerId)
    {
        return await context.PlayerGames.Where(pg => pg.Player.Id == playerId).Select(pg => pg.Game).ToListAsync();
    }

    public async Task<Player> CreatePlayerAsync(Player player)
    {
        await context.AddAsync(player);
        await context.SaveChangesAsync();
        return player;
    }
    public async Task<bool> PlayerExistsAsync(int playerId)
    {
        return await context.Players.AnyAsync(p => p.Id == playerId);
    }

}
