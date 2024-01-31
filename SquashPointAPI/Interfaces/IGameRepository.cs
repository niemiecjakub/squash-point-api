using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface IGameRepository
{
    ICollection<Game> GetAllGames();
    ICollection<Game> GetAllPlayerGames(int playerId);
    ICollection<Game> GetAllLeagueGames(int leagueId);
    Game GetGameById(int gameId);
    bool GameExists(int gameId);
    bool CreateGame(int leagueId, int player1Id, int player2Id);
    bool Save();
}