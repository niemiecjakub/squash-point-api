using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto.League;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeagueController(ILeagueRepository leagueRepository) : Controller
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<League>))]
    public IActionResult GetAllLeagues()
    {
        var leagues = leagueRepository.GetAllLeagues();
        var leagueDtos = leagues.Select(l => l.ToLeagueDto()).ToList();
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        return Ok(leagueDtos);
    }
    
    [HttpGet("name/{leagueName}")]
    [ProducesResponseType(200, Type = typeof(League))]
    public IActionResult GetLeagueByName(string leagueName)
    {
        var league = leagueRepository.GetLeagueByName(leagueName);
        var leagueDto = league.ToLeagueDto();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(leagueDto);
    }
    
    [HttpGet("{leagueId}")]
    [ProducesResponseType(200, Type = typeof(League))]
    [ProducesResponseType(400)]
    public IActionResult GetLeagueById(int leagueId)
    {
        if (!leagueRepository.LeagueExists(leagueId))
        {
            return NotFound();
        }
        var league = leagueRepository.GetLeagueById(leagueId);
        var leagueDto = league.ToLeagueDto();
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(leagueDto);
    }
    
    [HttpGet("players/{leagueId}")]
    public IActionResult GetAllLeaguePlayers(int leagueId)
    {
        if (!leagueRepository.LeagueExists(leagueId))
        {
            return NotFound();
        }
        var leaguePlayers = leagueRepository.GetAllLeaguePlayers(leagueId);
        var playerDtos = leaguePlayers.Select(p => p.ToPlayerDto()).ToList();
            
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(playerDtos);
    }
    
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateLeague([FromBody] CreateLeagueDto leagueCreate)
    {
        if (leagueCreate == null)
            return BadRequest(ModelState);

        var existingLeague = leagueRepository.GetAllLeagues()
            .FirstOrDefault(c => c.Name.Trim().ToUpper() == leagueCreate.Name.TrimEnd().ToUpper());

        if (existingLeague != null)
        {
            ModelState.AddModelError("", "League already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var league = leagueCreate.ToLeagueFromCreateDTO();

        if (!leagueRepository.CreateLeague(league))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }
    
    [HttpPost("addPlayer")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult AddPlayerToLeague([FromQuery] int leagueId, [FromQuery] int playerId)
    {   
        if (leagueRepository.IsPlayerInLeague(leagueId, playerId))
        {
            ModelState.AddModelError("", "Player is already in this league");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        
        if (!leagueRepository.AddPlayerToLeague(leagueId, playerId))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }
    
        return Ok("Successfully created");
    }
}