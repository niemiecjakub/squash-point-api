using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Data;
using SquashPointAPI.Dto.Game;
using SquashPointAPI.Helpers;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class GameRepository(ApplicationDBContext context) : IGameRepository
{
    public async Task<ICollection<Game>> GetGamesAsync(GameQueryObject gameQuery)
    {
        var games = context.Games
            .OrderByDescending(g => g.CreatedAt)
            .Include(g => g.League)
            .Include(g => g.PlayerGames)
            .ThenInclude(pg => pg.Player)
            .ThenInclude(p => p.Photo)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(gameQuery.GameStatus))
            games = games.Where(g => g.Status.Equals(gameQuery.GameStatus));

        if (gameQuery.WinnerId != null) games = games.Where(g => g.Winner.Id.Equals(gameQuery.WinnerId));

        if (gameQuery.OrderByScheduledDate) games = games.OrderByDescending(g => g.Date);

        if (gameQuery.OrderByCreateDate) games = games.OrderByDescending(g => g.CreatedAt);

        var skipNumber = (gameQuery.PageNumber - 1) * gameQuery.PageSize;
        return await games.Skip(skipNumber).Take(gameQuery.PageSize).ToListAsync();
    }

    public async Task<Game> GetGameByIdAsync(int gameId)
    {
        return await context.Games
            .Include(g => g.PlayerGames)
            .ThenInclude(pg => pg.Player)
            .ThenInclude(p => p.Photo)
            .Include(g => g.League)
            .Include(g => g.Sets)
            .ThenInclude(s => s.Points)
            .ThenInclude(p => p.Player)
            .FirstAsync(g => g.Id == gameId);
    }

    public async Task<bool> GameExistsAsync(int gameId)
    {
        return await context.Games.AnyAsync(g => g.Id == gameId);
    }

    public async Task<Game> CreateGameAsync(Game game, PlayerGame playerGame, PlayerGame opponentGame)
    {
        await context.Games.AddAsync(game);
        await context.PlayerGames.AddAsync(playerGame);
        await context.PlayerGames.AddAsync(opponentGame);
        await context.SaveChangesAsync();
        return game;
    }

    public async Task<Game> CreateGameAsyncTEST(Game game)
    {
        await context.Games.AddAsync(game);
        await context.SaveChangesAsync();
        return game;
    }

    public async Task<Game?> UpdateAsync(int gameId, UpdateGameRequestDto updateDto)
    {
        var existingGame = await context.Games
            .Include(g => g.PlayerGames)
            .ThenInclude(pg => pg.Player)
            .Include(g => g.League)
            .Include(g => g.Sets)
            .ThenInclude(s => s.Points)
            .ThenInclude(p => p.Player)
            .FirstAsync(g => g.Id == gameId);

        existingGame.Status = updateDto.Status;
        existingGame.Winner = await context.Players.FindAsync(updateDto.WinnerId);

        await context.SaveChangesAsync();

        return existingGame;
    }

    public async Task<Game?> DeleteAsync(int gameId)
    {
        var game = await context.Games.FirstOrDefaultAsync(g => g.Id == gameId);

        if (game == null) return null;

        context.Games.Remove(game);
        await context.SaveChangesAsync();
        return game;
    }

    public async Task<Game?> GetGameBySetId(int setId)
    {
        var set = await context.Set.FirstAsync(s => s.Id == setId);
        return await context.Games.Where(g => g.Sets.Contains(set)).Include(g => g.PlayerGames)
            .ThenInclude(pg => pg.Player)
            .Include(g => g.League)
            .Include(g => g.Sets)
            .ThenInclude(s => s.Points)
            .ThenInclude(p => p.Player)
            .FirstOrDefaultAsync();
    }
}