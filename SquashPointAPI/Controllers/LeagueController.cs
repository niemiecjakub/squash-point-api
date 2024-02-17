using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto.Game;
using SquashPointAPI.Dto.League;
using SquashPointAPI.Dto.Player;
using SquashPointAPI.Helpers;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeagueController(ILeagueRepository leagueRepository) : Controller
{
    [HttpGet("league-list")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<LeagueDto>))]
    public async Task<IActionResult> GetAllLeagues()
    {
        var leagues = await leagueRepository.GetLeaguesAsync();
        var leagueDtos = leagues.Select(l => l.ToLeagueDto()).ToList();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(leagueDtos);
    }

    [HttpGet("{leagueId}")]
    [ProducesResponseType(200, Type = typeof(LeagueDetailsDto))]
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

        var league = await leagueRepository.GetLeagueByIdAsync(leagueId);
        var leagueDetailsDto = league.ToLeagueDetailsDto();

        return Ok(leagueDetailsDto);
    }

    
    [HttpGet("{leagueId}/player-list")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<LeaguePlayerDto>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAllLeaguePlayers(int leagueId)
    {
        if (!await leagueRepository.LeagueExistsAsync(leagueId))
        {
            return NotFound();
        }

        var leaguePlayers = await leagueRepository.GetLeaguePlayersAsync(leagueId);
        var leaguePlayerDtos = leaguePlayers.Select(p => p.ToLeaguePlayerDto()).OrderByDescending(p => p.Score).ToList();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(leaguePlayerDtos);
    }

    [HttpGet("{leagueId}/league-games")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<GameDto>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAllLeagueGames(int leagueId, [FromQuery] GameQueryObject gameQuery)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await leagueRepository.LeagueExistsAsync(leagueId))
        {
            return NotFound();
        }

        var games = await leagueRepository.GetLeagueGamesAsync(leagueId, gameQuery);
        var gameDtos = games.Select(g => g.ToGameDto()).ToList();

        return Ok(gameDtos);
    }

    [HttpPost]
    [ProducesResponseType(200, Type = typeof(LeagueDto))]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateLeague([FromQuery] CreateLeagueDto leagueCreate)
    {
        if (leagueCreate == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var leagues = await leagueRepository.GetLeaguesAsync();
        var existingLeague =
            leagues.FirstOrDefault(c => c.Name.Trim().ToUpper() == leagueCreate.Name.TrimEnd().ToUpper());

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
    [ProducesResponseType(200, Type = typeof(PlayerLeague))]
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
        return Ok("Succesfully added");
    }
}