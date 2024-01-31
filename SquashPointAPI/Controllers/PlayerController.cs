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
    public IActionResult GetAllPlayers()
    {
        var players = playerRepository.GetPlayers();
        var playerDtos = players.Select(s => s.ToPlayerDto()).ToList();
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        return Ok(playerDtos);
    }
    
    [HttpGet("{firstName}/{lastName}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Player>))]
    public IActionResult GetPlayerByName(string firstName, string lastName)
    {
        var players = playerRepository.GetPlayers(firstName, lastName);
        var playerDtos = players.Select(s => s.ToPlayerDto()).ToList();
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(playerDtos);
    }
    
    [HttpGet("{playerId}")]
    [ProducesResponseType(200, Type = typeof(Player))]
    [ProducesResponseType(400)]
    public IActionResult GetPlayerById(int playerId)
    {
        if (!playerRepository.PlayerExists(playerId))
        {
            return NotFound();
        }
        var player = playerRepository.GetPlayer(playerId);
        var playerDto = player.ToPlayerDto();
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(playerDto);
    }
    
        
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreatePlayer([FromBody] CreatePlayerDto playerCreate)
    {
        if (playerCreate == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Use AutoMapper to map LeagueDto to League
        var player = playerCreate.ToPlayerFromCreateDTO();

        if (!playerRepository.CreatePlayer(player))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }
}