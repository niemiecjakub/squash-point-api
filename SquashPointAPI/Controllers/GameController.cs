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
    public async Task<IActionResult> GetAllGames()
    {
        var games = await gameRepository.GetAllGamesAsync();
        var gameDtos = games.Select(g => g.ToGameDto()).ToList();
        
        return Ok(gameDtos);
    }
    
    [HttpGet("league/{leagueId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAllLeagueGames(int leagueId)
    {
        if (!ModelState.IsValid)
        {   
            return BadRequest(ModelState);
        }
        
        if (!await leagueRepository.LeagueExistsAsync(leagueId))
        {
            return NotFound();
        }
        
        var games = await gameRepository.GetAllLeagueGamesAsync(leagueId);
        var gameDtos = games.Select(g => g.ToGameDto()).ToList();
        
        return Ok(gameDtos);
    }
    
    [HttpGet("player/{playerId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAllPlayerGames(int playerId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!await playerRepository.PlayerExistsAsync(playerId))
        {
            return NotFound();
        }
        
        var games = await gameRepository.GetAllPlayerGamesAsync(playerId);
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
        var gameDto = game.ToGameDto();
        
        return Ok(gameDto);
    }
    
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateGame([FromQuery] int leagueId, [FromQuery] int player1Id, [FromQuery] int player2Id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var game = await gameRepository.CreateGameAsync(leagueId, player1Id, player2Id);
        var gameDto = game.ToGameDto();
        
        return Ok(gameDto);
    }
}