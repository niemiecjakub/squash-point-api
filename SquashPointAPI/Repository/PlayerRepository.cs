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
}