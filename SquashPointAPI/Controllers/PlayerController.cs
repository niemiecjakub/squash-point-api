using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController(IPlayerRepository playerRepository, IMapper mapper) : Controller
{

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Player>))]
    public IActionResult GetAllPlayers()
    {
        var players = mapper.Map<List<PlayerDto>>(playerRepository.GetPlayers());
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        return Ok(players);
    }
    
    [HttpGet("{firstName}/{lastName}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Player>))]
    public IActionResult GetPlayerByName(string firstName, string lastName)
    {
        var players = mapper.Map<List<PlayerDto>>(playerRepository.GetPlayers(firstName, lastName));
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(players);
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
        var player = mapper.Map<PlayerDto>(playerRepository.GetPlayer(playerId));
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(player);
    }
    
        
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreatePlayer([FromBody] PlayerDto playerCreate)
    {
        if (playerCreate == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Use AutoMapper to map LeagueDto to League
        var playerMap = mapper.Map<Player>(playerCreate);

        if (!playerRepository.CreatePlayer(playerMap))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }
}