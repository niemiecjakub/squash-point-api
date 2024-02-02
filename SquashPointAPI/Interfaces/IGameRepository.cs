using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface IGameRepository
{
    Task<ICollection<Game>> GetAllGamesAsync();
    Task<Game> GetGameByIdAsync(int gameId);
    Task<bool> GameExistsAsync(int gameId);
    Task<Game> CreateGameAsync(int leagueId, int player1Id, int player2Id, DateTime date);
}