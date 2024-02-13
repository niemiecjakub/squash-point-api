using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto.Point;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Controllers;

public class PointController(ISetRepository setRepository, IPointRepository pointRepository) : Controller
{
    [HttpPost("addPoint")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreatePoint([FromQuery] CreatePointDto ceatePointDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await setRepository.SetExistsAsync(ceatePointDto.SetId))
        {
            return NotFound(); 
        }

        await pointRepository.CreatePointAsync(ceatePointDto);
        return Ok("Set created");
    }
}