using SquashPointAPI.Dto.Set;
using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface ISetRepository
{
    Task<Set>CreateSetAsync(CreateSetDto createSetDto);
    Task<Set>UpdateWinnerAsync(int playerId);
    Task<bool> SetExistsAsync(int setId);
}