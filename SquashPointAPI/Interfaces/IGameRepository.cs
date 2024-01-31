using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface IGameRepository
{
    public ICollection<Game> GetAllGames();
    public ICollection<Game> GetAllPlayerGames(int playerId);
    public ICollection<Game> GetAllLeagueGames(int leagueId);
    public Game GetGameById(int gameId);
    public bool GameExists(int gameId);
    public bool CreateGame(int leagueId, int player1Id, int player2Id);
    public bool Save();
}