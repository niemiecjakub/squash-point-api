using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController(IGameRepository gameRepository,IPlayerRepository playerRepository, ILeagueRepository leagueRepository) : Controller
{
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
    public IActionResult GetAllGames()
    {
        var games = gameRepository.GetAllGames();
        var gameDtos = games.Select(g => g.ToGameDto()).ToList();
        
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        return Ok(gameDtos);
    }
    
    [HttpGet("league/{leagueId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
    [ProducesResponseType(400)]
    public IActionResult GetAllLeagueGames(int leagueId)
    {
        if (!leagueRepository.LeagueExists(leagueId))
        {
            return NotFound();
        }
        
        var games = gameRepository.GetAllLeagueGames(leagueId);
        var gameDtos = games.Select(g => g.ToGameDto()).ToList();
        
        if (!ModelState.IsValid)
        {   
            return BadRequest(ModelState);
        }
        
        return Ok(gameDtos);
    }
    
    [HttpGet("player/{playerId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
    [ProducesResponseType(400)]
    public IActionResult GetAllPlayerGames(int playerId)
    {
        if (!playerRepository.PlayerExists(playerId))
        {
            return NotFound();
        }
        
        var games = gameRepository.GetAllPlayerGames(playerId);
        var gameDtos = games.Select(g => g.ToGameDto()).ToList();
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        return Ok(gameDtos);
    }
    
    [HttpGet("{gameId}")]
    [ProducesResponseType(200, Type = typeof(Game))]
    [ProducesResponseType(400)]
    public IActionResult GetGameById(int gameId)
    {
        if (!gameRepository.GameExists(gameId))
        {
            return NotFound();
        }
        
        var games = gameRepository.GetGameById(gameId);
        var gameDto = games.ToGameDto();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(gameDto);
    }
    
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateGame([FromQuery] int leagueId, [FromQuery] int player1Id, [FromQuery] int player2Id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!gameRepository.CreateGame(leagueId, player1Id, player2Id))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }
}