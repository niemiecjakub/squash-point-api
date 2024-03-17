using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto.Game;
using SquashPointAPI.Extensions;
using SquashPointAPI.Helpers;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController(
    IGameRepository gameRepository,
    ILeagueRepository leagueRepository,
    IPlayerRepository playerRepository,
    UserManager<Player> userManager) : Controller
{
    [HttpGet("all")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<GameDto>))]
    public async Task<IActionResult> GetGames([FromQuery] GameQueryObject gameGameQuery)
    {
        var games = await gameRepository.GetGamesAsync(gameGameQuery);
        var gameDtos = games.Select(g => g.ToGameDto()).ToList();

        return Ok(gameDtos);
    }

    [HttpGet("{gameId}")]
    [ProducesResponseType(200, Type = typeof(GameDetailsDto))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetGameById(int gameId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await gameRepository.GameExistsAsync(gameId)) return NotFound();

        var game = await gameRepository.GetGameByIdAsync(gameId);
        var gameDto = game.ToGameDetailsDto();

        return Ok(gameDto);
    }

    [HttpGet("{gameId}/summary")]
    [ProducesResponseType(200, Type = typeof(GameSummaryDto))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetGameSummaryById(int gameId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await gameRepository.GameExistsAsync(gameId)) return NotFound();

        var game = await gameRepository.GetGameByIdAsync(gameId);

        var gameDto = game.ToGameSummaryDto();

        return Ok(gameDto);
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateGame([FromQuery] int leagueId,
        [FromQuery] string opponentId, [FromQuery] DateTime date)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var league = await leagueRepository.GetLeagueByIdAsync(leagueId);
        var userEmail = User.GetUserEmail();
        var player = userManager.FindByEmailAsync(userEmail).Result;
        var opponent = await playerRepository.GetPlayerAsync(opponentId);

        var game = new Game
        {
            League = league,
            Status = "Unfinished",
            Date = date
        };
        var playerGame = new PlayerGame
        {
            Player = player,
            Game = game
        };
        var opponentGame = new PlayerGame
        {
            Player = opponent,
            Game = game
        };

        if (game == null || playerGame == null || opponentGame == null)
        {
            return BadRequest("Something went wrong");
        }

        await gameRepository.CreateGameAsync(game, playerGame, opponentGame);
        var gameDto = game.ToGameDto();

        return Ok(gameDto);
    }

    [HttpPut]
    [Authorize]
    [Route("{gameId:int}")]
    public async Task<IActionResult> UpdateGame([FromRoute] int gameId, [FromBody] UpdateGameRequestDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var game = await gameRepository.UpdateAsync(gameId, updateDto);

        if (game == null) return NotFound("Comment not found");

        return Ok(game.ToGameDto());
    }

    [HttpDelete]
    [Authorize]
    [Route("{gameId:int}")]
    public async Task<IActionResult> DeleteGame([FromRoute] int gameId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var game = await gameRepository.DeleteAsync(gameId);

        if (game == null) return NotFound("Game doesn't exist");

        return Ok(game);
    }
}