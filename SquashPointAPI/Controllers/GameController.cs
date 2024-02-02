using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController(IGameRepository gameRepository,IPlayerRepository playerRepository, ILeagueRepository leagueRepository) : Controller
{
    
    [HttpGet("game-list")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
    public async Task<IActionResult> GetAllGames()
    {
        var games = await gameRepository.GetAllGamesAsync();
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
    public async Task<IActionResult> CreateGame([FromQuery] int leagueId, [FromQuery] int player1Id, [FromQuery] int player2Id, [FromQuery] int year, [FromQuery] int month, [FromQuery] int day, [FromQuery] int hour, [FromQuery] int minute)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        DateTime date = new DateTime(year, month, day, hour, minute, 0);
        var game = await gameRepository.CreateGameAsync(leagueId, player1Id, player2Id, date);
        var gameDto = game.ToGameDto();
        
        return Ok(gameDto);
    }
}