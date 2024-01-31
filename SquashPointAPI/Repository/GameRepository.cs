using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Data;
using SquashPointAPI.Dto;
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
    
    public bool CreateGame(int leagueId, int player1Id, int player2Id)
    {
        var league = context.Leagues.Find(leagueId);
        var player1 = context.Players.Find(player1Id);
        var player2 = context.Players.Find(player2Id);

        if (league == null || player1 == null || player2 == null)
        {
            return false;
        }

        var newGame = new Game
        {
            League = league,
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
        context.Games.Add(newGame);
        context.PlayerGames.Add(playerGame1);
        context.PlayerGames.Add(playerGame2);
        return Save();
    }
    public bool Save()
    {
        var saved = context.SaveChanges();
        return saved > 0 ? true : false;
    }
}