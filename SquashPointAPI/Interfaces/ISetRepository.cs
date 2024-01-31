using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface ISetRepository
{
    Task<Set>CreateSetAsync(int gameId);
    Task<Set>UpdateWinnerAsync(int playerId);
}