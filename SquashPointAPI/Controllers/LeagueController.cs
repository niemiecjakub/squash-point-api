using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;
using SquashPointAPI.Repository;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeagueController(ILeagueRepository leagueRepository,IPlayerRepository playerRepository ,IMapper mapper) : Controller
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<League>))]
    public IActionResult GetAllLeagues()
    {
        var leagues = mapper.Map<List<LeagueDto>>(leagueRepository.GetAllLeagues());
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        return Ok(leagues);
    }
    
    [HttpGet("name/{leagueName}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<League>))]
    public IActionResult GetLeagueByName(string leagueName)
    {
        var league = mapper.Map<LeagueDto>(leagueRepository.GetLeagueByName(leagueName));
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(league);
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
        var league = mapper.Map<LeagueDto>(leagueRepository.GetLeagueById(leagueId));
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(league);
    }
    
    [HttpGet("players/{leagueId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Player>))]
    [ProducesResponseType(400)]
    public IActionResult GetAllLeaguePlayers(int leagueId)
    {
        if (!leagueRepository.LeagueExists(leagueId))
        {
            return NotFound();
        }
        var leaguePlayers = mapper.Map<List<PlayerDto>>(leagueRepository.GetAllLeaguePlayers(leagueId));
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(leaguePlayers);
    }
    
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateLeague([FromBody] LeagueDto leagueCreate)
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

        // Use AutoMapper to map LeagueDto to League
        var leagueMap = mapper.Map<League>(leagueCreate);

        if (!leagueRepository.CreateLeague(leagueMap))
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
        var league = leagueRepository.GetLeagueById(leagueId);
        var player = playerRepository.GetPlayer(playerId);
        
        // if (pokemons != null)
        // {
        //     ModelState.AddModelError("", "Owner already exists");
        //     return StatusCode(422, ModelState);
        // }
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        
        if (!leagueRepository.AddPlayerToLeague(leagueId, playerId))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }

}