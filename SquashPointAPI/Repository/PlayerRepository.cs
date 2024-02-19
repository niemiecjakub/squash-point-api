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
            .Include(p => p.PlayerLeagues)
            .ThenInclude(pg => pg.League)
            .Include(p => p.PlayerGames)
            .ThenInclude(pg => pg.Game)
            .ThenInclude(g => g.PlayerGames)
            .ThenInclude(pg => pg.Player)
            .Include(p => p.PlayerGames)
            .ThenInclude(pg => pg.Game)
            .ThenInclude(pg => pg.League)
            .FirstAsync(p => p.Id == playerId);
    }

    public async Task<ICollection<Game>> GetAllPlayerGamesAsync(int playerId)
    {
        return await context.Games
            .Include(g => g.League)
            .Include(g => g.PlayerGames)
            .ThenInclude(pg => pg.Player)
            .Where(g => g.PlayerGames.Any(pg => pg.PlayerId == playerId))
            .ToListAsync();
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

    public async Task<bool> EmailAlreadyTakenAsync(string email)
    {
        return await context.Players.AnyAsync(p => p.Email == email);
    }
}