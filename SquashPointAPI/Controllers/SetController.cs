using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Interfaces;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SetController(ISetRepository setRepository) : Controller
{
    [HttpPost("addSet")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateSet([FromQuery] int gameId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var set = await setRepository.CreateSetAsync(gameId);
        return Ok(set);
    }
}