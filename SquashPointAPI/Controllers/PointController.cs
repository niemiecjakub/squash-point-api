using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto.Point;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PointController(ISetRepository setRepository, IPointRepository pointRepository) : Controller
{
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreatePoint([FromQuery] CreatePointDto pointCreate)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await setRepository.SetExistsAsync(pointCreate.SetId)) return NotFound();

        var point = await pointRepository.CreatePointAsync(pointCreate);
        var pointDto = point.ToPointDto();
        return Ok(pointDto);
    }
}