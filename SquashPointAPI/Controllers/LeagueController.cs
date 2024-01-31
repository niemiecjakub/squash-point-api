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
    public async Task<IActionResult> GetAllLeagues()
    {
        var leagues = await leagueRepository.GetAllLeaguesAsync();
        var leagueDtos = leagues.Select(l => l.ToLeagueDto()).ToList();
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        return Ok(leagueDtos);
    }
    
    [HttpGet("name/{leagueName}")]
    [ProducesResponseType(200, Type = typeof(League))]
    public async Task<IActionResult> GetLeagueByName(string leagueName)
    {
        var league = await leagueRepository.GetLeagueByNameAsync(leagueName);
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
    public async Task<IActionResult> GetLeagueById(int leagueId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!await leagueRepository.LeagueExistsAsync(leagueId))
        {
            return NotFound();
        }
        var league =await leagueRepository.GetLeagueByIdAsync(leagueId);
        var leagueDetailsDto = league.ToLeagueDetailsDto();

        return Ok(leagueDetailsDto);
    }
    
    [HttpGet("players/{leagueId}")]
    public async Task<IActionResult> GetAllLeaguePlayers(int leagueId)
    {
        if (!await leagueRepository.LeagueExistsAsync(leagueId))
        {
            return NotFound();
        }
        var leaguePlayers = await leagueRepository.GetAllLeaguePlayersAsync(leagueId);
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
    public async Task<IActionResult> CreateLeague([FromBody] CreateLeagueDto leagueCreate)
    {
        if (leagueCreate == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var leagues = await leagueRepository.GetAllLeaguesAsync();
        var existingLeague = leagues.FirstOrDefault(c => c.Name.Trim().ToUpper() == leagueCreate.Name.TrimEnd().ToUpper());

        if (existingLeague != null)
        {
            ModelState.AddModelError("", "League already exists");
            return StatusCode(422, ModelState);
        }
        
        var league = leagueCreate.ToLeagueFromCreateDTO();
        await leagueRepository.CreateLeagueAsync(league);
        var leagueDto = league.ToLeagueDto();

        return Ok(leagueDto);
    }
    
    [HttpPost("addPlayer")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddPlayerToLeague([FromQuery] int leagueId, [FromQuery] int playerId)
    {   
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (await leagueRepository.IsPlayerInLeagueAsync(leagueId, playerId))
        {
            ModelState.AddModelError("", "Player is already in this league");
            return StatusCode(422, ModelState);
        }

        var playerLeague = await leagueRepository.AddPlayerToLeagueAsync(leagueId, playerId);
        return Ok(playerLeague);
    }
}