using SquashPointAPI.Dto.Game;
using SquashPointAPI.Helpers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface IGameRepository
{
    Task<ICollection<Game>> GetGamesAsync(GameQueryObject gameQuery);
    Task<Game> GetGameByIdAsync(int gameId);
    Task<bool> GameExistsAsync(int gameId);
    Task<Game> CreateGameAsync(int leagueId, int player1Id, int player2Id, DateTime date);
    Task<Game?> UpdateAsync(int gameId, UpdateGameRequestDto updateDto);
    Task<Game?> DeleteAsync(int gameId);
}