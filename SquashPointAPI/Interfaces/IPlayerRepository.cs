using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface IPlayerRepository
{
    ICollection<Player> GetPlayers();
}