using SquashPointAPI.Data;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class PlayerRepository(DataContext context) : IPlayerRepository
{
    public ICollection<Player> GetPlayers()
    {
        return context.Players.OrderBy(p => p.Id).ToList();
    }
    public ICollection<Player> GetPlayers(string firstName, string lastName)
    {
        return context.Players.Where(p => p.FirstName == firstName && p.LastName == lastName).ToList();
    }
    public Player GetPlayer(int playerId)
    {
        return context.Players.Where(p => p.Id == playerId).FirstOrDefault();
    }
    public bool PlayerExists(int playerId)
    {
        return context.Players.Any(p => p.Id == playerId);
    }
    public bool CreatePlayer(Player player)
    {
        context.Add(player);
        return Save();
    }
    public bool Save()
    {
        var saved = context.SaveChanges();
        return saved > 0 ? true : false;
    }
}
