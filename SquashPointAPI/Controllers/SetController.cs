using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto.Set;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;

namespace SquashPointAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SetController(
    ISetRepository setRepository,
    IGameRepository gameRepository,
    IPlayerRepository playerRepository) : Controller
{
    /// <summary>
    /// Create new set 
    /// </summary>
    /// <param name="setCreate"></param>
    /// <returns></returns>
    /// <response code="200">OK</response>
    /// <response code="400">Bad request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Game with given ID not found</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(200, Type = typeof(SetDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
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

    /// <summary>
    /// Update set winner
    /// </summary>
    /// <param name="setId"></param>
    /// <param name="updateDto"></param>
    /// <returns></returns>
    /// <response code="200">OK</response>
    /// <response code="400">Bad request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Set with given ID not found</response>
    [HttpPut]
    [Authorize]
    [Route("{setId:int}")]
    [ProducesResponseType(200, Type = typeof(SetDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateSet([FromRoute] [Required] int setId,
        [FromBody] UpdateSetRequestDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await setRepository.SetExistsAsync(setId)) return NotFound("Set not found");

        var set = await setRepository.UpdateWinnerAsync(setId, updateDto);
        var setDto = set.ToSetDto();
        return Ok(setDto);
    }

    /// <summary>
    /// Get detailed set summary 
    /// </summary>
    /// <param name="setId"></param>
    /// <returns></returns>
    /// <response code="200">OK</response>
    /// <response code="400">Bad request</response>
    /// <response code="404">Set with given ID not found</response>
    [HttpGet]
    [Route("{setId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetSetSummaryById([FromRoute] [Required] int setId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await setRepository.SetExistsAsync(setId)) return NotFound("Set not found");

        var game = await gameRepository.GetGameBySetId(setId);
        var set = await setRepository.GetSetAsync(setId);
        var players = game.ToGameDto().Players.ToList();

        var setSummary = new
        {
            Id = set.Id,
            CreatedAt = set.CreatedAt,
            Winner = new
            {
                FullName = $"{set.Winner?.FirstName} {set.Winner?.LastName}",
                Id = set.Winner?.Id,
            },
            Player1 = new
            {
                Name = players[0].FullName,
                Points = set.Points?.Where(p => p.Player.Id == players[0].Id).Count()
            },
            Player2 = new
            {
                Name = players[1].FullName,
                Points = set.Points?.Where(p => p.Player.Id == players[1].Id).Count()
            }
        };

        return Ok(setSummary);
    }
}