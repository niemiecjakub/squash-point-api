using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto.Set;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SetController(ISetRepository setRepository, IGameRepository gameRepository) : Controller
{
    [HttpPost("addSet")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateSet([FromQuery] CreateSetDto setCreate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await gameRepository.GameExistsAsync(setCreate.GameId))
        {
            return NotFound(); 
        }
        
        var set = await setRepository.CreateSetAsync(setCreate);
        var setDto = set.ToSetDto();
        return Ok(setDto);
    }
}