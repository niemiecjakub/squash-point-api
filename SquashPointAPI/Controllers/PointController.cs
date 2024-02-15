using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto.Point;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Controllers;

public class PointController(ISetRepository setRepository, IPointRepository pointRepository) : Controller
{
    [HttpPost("addPoint")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreatePoint([FromQuery] CreatePointDto pointCreate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await setRepository.SetExistsAsync(pointCreate.SetId))
        {
            return NotFound(); 
        }

        var point = await pointRepository.CreatePointAsync(pointCreate);
        var pointDto = point.ToPointDto();
        return Ok(pointDto);
    }
}