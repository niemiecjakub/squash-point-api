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

    [HttpGet]
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
    
    [HttpGet("{firstName}/{lastName}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Player>))]
    public async Task<IActionResult> GetPlayerByName(string firstName, string lastName)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var players = await playerRepository.GetPlayersAsync(firstName, lastName);
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
        var playerDto = player.ToPlayerDto();

        return Ok(playerDto);
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