using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Data;
using SquashPointAPI.Dto;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class GameRepository(DataContext context) : IGameRepository
{
    public async Task<ICollection<Game>> GetAllGamesAsync()
    {
        return await context.Games
            .OrderByDescending(g => g.CreatedAt)
            .Include(g => g.League)
            .Include(g => g.PlayerGames)
            .ThenInclude(pg => pg.Player)
            .ToListAsync();
    }

    public async Task<Game> GetGameByIdAsync(int gameId)
    {
        return await context.Games
            .Include(g => g.PlayerGames)
            .ThenInclude(pg => pg.Player)
            .Include(g =>g.League)
            .Include(g => g.Sets)
            .ThenInclude(s => s.Points)
            .ThenInclude(p => p.Winner)
            .FirstAsync(g => g.Id == gameId);
    }

    public async Task<bool> GameExistsAsync(int gameId)
    {
        return await context.Games.AnyAsync(g => g.Id == gameId);
    }
    
    public async Task<Game> CreateGameAsync(int leagueId, int player1Id, int player2Id, DateTime date)
    {
        var league = await context.Leagues.FindAsync(leagueId);
        var player1 = await context.Players.FindAsync(player1Id);
        var player2 = await context.Players.FindAsync(player2Id);

        var newGame = new Game
        {
            League = league,
            Status = "Unfinished",
            Date = date
        };
        var playerGame1 = new PlayerGame()
        {
            Player = player1,
            Game = newGame
        };
        var playerGame2 = new PlayerGame()
        {
            Player = player2,
            Game = newGame
        };
        await context.Games.AddAsync(newGame);
        await context.PlayerGames.AddAsync(playerGame1);
        await context.PlayerGames.AddAsync(playerGame2);
        await context.SaveChangesAsync();
        
        return newGame;
    }
}