using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeagueController(ILeagueRepository leagueRepository, IMapper mapper) : Controller
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
}