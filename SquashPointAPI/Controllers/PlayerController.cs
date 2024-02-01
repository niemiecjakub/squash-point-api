using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto.Player;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController(IPlayerRepository playerRepository) : Controller
{
    
    [HttpGet("player-list")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Player>))]
    public async Task<IActionResult> GetAllPlayers()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var players = await playerRepository.GetPlayersAsync();
        var playerDtos = players.Select(s => s.ToPlayerDto()).ToList();
        
        return Ok(playerDtos);
    }
    
    [HttpGet("{playerId}")]
    [ProducesResponseType(200, Type = typeof(Player))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetPlayerById(int playerId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!await playerRepository.PlayerExistsAsync(playerId))
        {
            return NotFound();
        }
        var player = await playerRepository.GetPlayerAsync(playerId);
        var playerDto = player.ToPlayerDetailsDto();

        return Ok(playerDto);
    }
    
    [HttpGet("{playerId}/player-games")]
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
        
        var games = await playerRepository.GetAllPlayerGamesAsync(playerId);
        var gameDtos = games.Select(g => g.ToGameDto()).ToList();
        
        return Ok(gameDtos);
    }
    
        
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerDto playerCreate)
    {
        if (playerCreate == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Use AutoMapper to map LeagueDto to League
        var player = playerCreate.ToPlayerFromCreateDTO();
        await playerRepository.CreatePlayerAsync(player);
        var playerDto = player.ToPlayerDto();

        return Ok(playerDto);
    }
}