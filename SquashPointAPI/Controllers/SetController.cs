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
    public IActionResult CreateSet([FromQuery] int gameId)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!setRepository.CreateSet(gameId))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }
    
        return Ok("Successfully created");
    }
}