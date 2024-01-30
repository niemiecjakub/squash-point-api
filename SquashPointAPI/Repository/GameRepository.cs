using SquashPointAPI.Data;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class GameRepository(DataContext context) : IGameRepository
{
    public ICollection<Game> GetAllGames()
    {
        return context.Games.OrderBy(g => g.Id).ToList();
    }

    public ICollection<Game> GetAllPlayerGames(int playerId)
    {
        return context.PlayerGames.Where(pg => pg.Player.Id == playerId).Select(pg => pg.Game).ToList();
        return context.Games.Where(g => g.PlayerGames.Any(p => p.PlayerId == playerId)).ToList();
    }

    public ICollection<Game> GetAllLeagueGames(int leagueId)
    {
        return context.Games.Where(g => g.League.Id == leagueId).ToList();
    }

    public Game GetGameById(int gameId)
    {
        return context.Games.Where(g => g.Id == gameId).FirstOrDefault();
    }

    public bool GameExists(int gameId)
    {
        return context.Games.Any(g => g.Id == gameId);
    }
}