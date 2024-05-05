using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto.Point;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;

namespace SquashPointAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PointController(
    ISetRepository setRepository,
    IPointRepository pointRepository,
    IPlayerRepository playerRepository) : Controller
{
    /// <summary>
    /// Create point
    /// </summary>
    /// <param name="createPointDto"></param>
    /// <returns></returns>
    /// <response code="200">OK</response>
    /// <response code="400">If error occured</response>
    /// <response code="401">If error occured</response>
    /// <response code="404">Set with given ID not found</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> CreatePoint([FromQuery] CreatePointDto createPointDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await setRepository.SetExistsAsync(createPointDto.SetId)) return NotFound();

        var player = await playerRepository.GetPlayerAsync(createPointDto.PlayerId);
        var set = await setRepository.GetSetAsync(createPointDto.SetId);
        var point = createPointDto.ToPointFromCreateDto(player, set);

        await pointRepository.CreatePointAsync(point);

        var pointDto = point.ToPointDto();
        return Ok(pointDto);
    }
}