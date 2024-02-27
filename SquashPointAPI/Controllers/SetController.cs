using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto.Set;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SetController(ISetRepository setRepository, IGameRepository gameRepository, IPlayerRepository playerRepository) : Controller
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateSet([FromQuery] CreateSetDto setCreate)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await gameRepository.GameExistsAsync(setCreate.GameId)) return NotFound();


        var game = await gameRepository.GetGameByIdAsync(setCreate.GameId);
        var winner = await playerRepository.GetPlayerAsync(setCreate.WinnerId);
        var set = setCreate.ToSetFromCreateDto(game, winner);
        
        await setRepository.CreateSetAsync(set);
        
        var setDto = set.ToSetDto();
        return Ok(setDto);
    }

    [HttpPut]
    [Authorize]
    [Route("{setId:int}")]
    public async Task<IActionResult> Update([FromRoute] int setId, [FromBody] UpdateSetRequestDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await setRepository.SetExistsAsync(setId)) return NotFound("Set not found");
        
        var set = await setRepository.UpdateWinnerAsync(setId, updateDto);

        return Ok(set.ToSetDto());
    }
}