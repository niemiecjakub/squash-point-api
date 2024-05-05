using SquashPointAPI.Dto.Set;
using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface ISetRepository
{
    Task<Set> GetSetAsync(int setId);
    Task<Set> CreateSetAsync(Set set);
    Task<Set> UpdateWinnerAsync(int setId, UpdateSetRequestDto updateDto);
    Task<bool> SetExistsAsync(int setId);
    
}