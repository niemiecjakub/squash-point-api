using SquashPointAPI.Dto.Game;
using SquashPointAPI.Helpers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface IGameRepository
{
    Task<ICollection<Game>> GetGamesAsync(GameQueryObject gameQuery);
    Task<Game> GetGameByIdAsync(int gameId);
    Task<bool> GameExistsAsync(int gameId);
    Task<Game> CreateGameAsync(Game game, PlayerGame playerGame, PlayerGame opponentGame);
    Task<Game> CreateGameAsyncTEST(Game game);
    Task<Game?> UpdateAsync(int gameId, UpdateGameRequestDto updateDto);
    Task<Game?> DeleteAsync(int gameId);
    Task<Game?> GetGameBySetId(int setId);
}