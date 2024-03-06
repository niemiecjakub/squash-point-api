using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto.Set;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SetController(
    ISetRepository setRepository,
    IGameRepository gameRepository,
    IPlayerRepository playerRepository) : Controller
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
    public async Task<IActionResult> UpdateSet([FromRoute] int setId, [FromBody] UpdateSetRequestDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await setRepository.SetExistsAsync(setId)) return NotFound("Set not found");

        var set = await setRepository.UpdateWinnerAsync(setId, updateDto);
        var setDto = set.ToSetDto();
        return Ok(setDto);
    }

    [HttpGet]
    [Route("{setId:int}")]
    public async Task<IActionResult> GetSetSummaryById([FromRoute] int setId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await setRepository.SetExistsAsync(setId)) return NotFound("Set not found");

        var game = await gameRepository.GetGameBySetId(setId);
        var set = await setRepository.GetSetAsync(setId);
        var players = game.ToGameDto().Players.ToList();

        Console.WriteLine(players[0].FullName);
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
                Points = set.Points?.Where(p => p.Winner.Id == players[0].Id).Count()
            },
            Player2 = new
            {
                Name = players[1].FullName,
                Points = set.Points?.Where(p => p.Winner.Id == players[1].Id).Count()
            }
        };

        return Ok(setSummary);
    }
}