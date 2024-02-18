using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto.Game;
using SquashPointAPI.Helpers;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController(IGameRepository gameRepository) : Controller
{
    [HttpGet("game-list")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
    public async Task<IActionResult> GetAllGames([FromQuery] GameQueryObject gameGameQuery)
    {
        var games = await gameRepository.GetGamesAsync(gameGameQuery);
        var gameDtos = games.Select(g => g.ToGameDto()).ToList();

        return Ok(gameDtos);
    }

    [HttpGet("{gameId}")]
    [ProducesResponseType(200, Type = typeof(Game))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetGameById(int gameId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await gameRepository.GameExistsAsync(gameId))
        {
            return NotFound();
        }

        var game = await gameRepository.GetGameByIdAsync(gameId);
        var gameDto = game.ToGameDetailsDto();

        return Ok(gameDto);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateGame([FromQuery] int leagueId, [FromQuery] int player1Id,
        [FromQuery] int player2Id, [FromQuery] int year, [FromQuery] int month, [FromQuery] int day,
        [FromQuery] int hour, [FromQuery] int minute)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (player1Id == player2Id)
        {
            return BadRequest(ModelState);
        }

        DateTime date = new DateTime(year, month, day, hour, minute, 0);

        var game = await gameRepository.CreateGameAsync(leagueId, player1Id, player2Id, date);
        var gameDto = game.ToGameDto();

        return Ok(gameDto);
    }
    
    [HttpPut]
    [Route("{gameId:int}")]
    public async Task<IActionResult> Update([FromRoute] int gameId, [FromBody] UpdateGameRequestDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var game = await gameRepository.UpdateAsync(gameId, updateDto);

        if (game == null)
        {
            return NotFound("Comment not found");
        }

        return Ok(game.ToGameDto());
    }
    
    [HttpDelete]
    [Route("{gameId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int gameId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var game = await gameRepository.DeleteAsync(gameId);

        if (game == null)
        {
            return NotFound("Game doesn't exist");
        }

        return Ok(game);
    }
}