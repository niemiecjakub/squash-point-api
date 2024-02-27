using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto.Point;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PointController(ISetRepository setRepository, IPointRepository pointRepository, IPlayerRepository playerRepository) : Controller
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreatePoint([FromQuery] CreatePointDto createPointDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await setRepository.SetExistsAsync(createPointDto.SetId)) return NotFound();
        
        var winner = await playerRepository.GetPlayerAsync(createPointDto.WinnerId);
        var set = await setRepository.GetSetAsync(createPointDto.SetId);
        var point = createPointDto.ToPointFromCreateDto(winner, set);
        
        await pointRepository.CreatePointAsync(point);
        
        var pointDto = point.ToPointDto();
        return Ok(pointDto);
    }
}