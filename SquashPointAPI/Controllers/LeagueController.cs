using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto.Game;
using SquashPointAPI.Dto.League;
using SquashPointAPI.Dto.Player;
using SquashPointAPI.Extensions;
using SquashPointAPI.Helpers;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeagueController(ILeagueRepository leagueRepository, UserManager<Player> userManager) : Controller
{
    [HttpGet("all")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<LeagueDto>))]
    public async Task<IActionResult> GetLeagues()
    {
        var leagues = await leagueRepository.GetLeaguesAsync();
        var leagueDtos = leagues.Select(l => l.ToLeagueDto()).ToList();

        if (!ModelState.IsValid) return BadRequest(ModelState);

        return Ok(leagueDtos);
    }

    [HttpGet("{leagueId}")]
    [ProducesResponseType(200, Type = typeof(LeagueDetailsDto))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetLeagueById(int leagueId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await leagueRepository.LeagueExistsAsync(leagueId)) return NotFound();

        var league = await leagueRepository.GetLeagueByIdAsync(leagueId);

        var leagueDetailsDto = league.ToLeagueDetailsDto();

        return Ok(leagueDetailsDto);
    }


    [HttpGet("{leagueId}/players")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<LeaguePlayerDto>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetLeaguePlayers(int leagueId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await leagueRepository.LeagueExistsAsync(leagueId)) return NotFound();

        var leaguePlayers = await leagueRepository.GetLeaguePlayersAsync(leagueId);
        var leaguePlayerDtos =
            leaguePlayers.Select(p => p.ToLeaguePlayerDto()).OrderByDescending(p => p.Score).ToList();

        return Ok(leaguePlayerDtos);
    }

    [HttpGet("{leagueId}/games")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<GameDto>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAllLeagueGames(int leagueId, [FromQuery] GameQueryObject gameQuery)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await leagueRepository.LeagueExistsAsync(leagueId)) return NotFound();

        var leagueGames = await leagueRepository.GetLeagueGamesAsync(leagueId, gameQuery);
        var leagueGamesDto = leagueGames.Select(g => g.ToGameDto()).ToList();

        return Ok(leagueGamesDto);
    }

    [HttpPost]
    [Authorize]
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
        var userEmail = User.GetUserEmail();
        var owner = userManager.FindByEmailAsync(userEmail).Result;
        var league = leagueCreate.ToLeagueFromCreateDTO(owner);
        await leagueRepository.CreateLeagueAsync(league);

        return Ok("created");
    }

    [HttpPost("join")]
    [Authorize]
    [ProducesResponseType(200, Type = typeof(PlayerLeague))]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> JoinLeague([FromQuery] int leagueId)
    {
        var userEmail = User.GetUserEmail();
        var player = userManager.FindByEmailAsync(userEmail).Result;

        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await leagueRepository.LeagueExistsAsync(leagueId))
        {
            return BadRequest("League doesnt exist");
        }

        if (await leagueRepository.IsPlayerInLeagueAsync(leagueId, player.Id))
        {
            ModelState.AddModelError("", "Player is already in this league");
            return StatusCode(422, ModelState);
        }

        var playerLeague = new PlayerLeague()
        {
            LeagueId = leagueId,
            PlayerId = player.Id,
            Score = 0
        };
        await leagueRepository.AddPlayerToLeagueAsync(playerLeague);

        return Created();
    }

    [HttpPost("leave")]
    [Authorize]
    public async Task<IActionResult> LeaveLeague([FromQuery] int leagueId)
    {
        var userEmail = User.GetUserEmail();
        var player = userManager.FindByEmailAsync(userEmail).Result;

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var playerLeague = await leagueRepository.RemovePlayerAsync(leagueId, player.Id);

        if (playerLeague == null) return NotFound("This player isnt part of this league");

        return Ok("Player removed");
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteLeague([FromQuery] int leagueId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var league = await leagueRepository.DeleteAsync(leagueId);

        if (league == null) return NotFound("League not found");

        return Ok("League removed");
    }
}