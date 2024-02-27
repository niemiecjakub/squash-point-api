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
public class GameController(IGameRepository gameRepository, ILeagueRepository leagueRepository, IPlayerRepository playerRepository, UserManager<Player> userManager) : Controller
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
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await gameRepository.GameExistsAsync(gameId)) return NotFound();

        var game = await gameRepository.GetGameByIdAsync(gameId);
        var gameDto = game.ToGameDetailsDto();

        return Ok(gameDto);
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateGame([FromQuery] int leagueId, 
        [FromQuery] string player2Id, [FromQuery] int year, [FromQuery] int month, [FromQuery] int day,
        [FromQuery] int hour, [FromQuery] int minute)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var league = await leagueRepository.GetLeagueByIdAsync(leagueId);
        var userEmail = User.GetUserEmail();
        var player = userManager.FindByEmailAsync(userEmail).Result;
        var opponent = await playerRepository.GetPlayerAsync(player2Id);
        var date = new DateTime(year, month, day, hour, minute, 0);
        
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
    public async Task<IActionResult> Update([FromRoute] int gameId, [FromBody] UpdateGameRequestDto updateDto)
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
    public async Task<IActionResult> Delete([FromRoute] int gameId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var game = await gameRepository.DeleteAsync(gameId);

        if (game == null) return NotFound("Game doesn't exist");

        return Ok(game);
    }
}