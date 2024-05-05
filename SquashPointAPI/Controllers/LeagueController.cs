using System.ComponentModel.DataAnnotations;
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

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LeagueController(
    ILeagueRepository leagueRepository,
    IImageRepository imageRepository,
    UserManager<Player> userManager) : Controller
{
    /// <summary>
    /// Get list of all leagues 
    /// </summary>
    /// <response code="200">OK</response>
    /// <response code="400">Bad request</response>
    [HttpGet("all")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<LeagueDto>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetLeagues()
    {
        var leagues = await leagueRepository.GetLeaguesAsync();
        var leagueDtos = leagues.Select(l => l.ToLeagueDto()).ToList();

        if (!ModelState.IsValid) return BadRequest(ModelState);

        return Ok(leagueDtos);
    }

    /// <summary>
    /// Get specific league details
    /// </summary>
    /// <response code="200">OK</response>
    /// <response code="400">Bad request</response>
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


    /// <summary>
    /// Get all players enrolled for specific league
    /// </summary>
    /// <response code="200">OK</response>
    /// <response code="400">Bad request</response>
    /// <response code="404">League not found</response>
    [HttpGet("{leagueId}/players")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<LeaguePlayerDto>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetLeaguePlayers(int leagueId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await leagueRepository.LeagueExistsAsync(leagueId)) return NotFound();

        var leaguePlayers = await leagueRepository.GetLeaguePlayersAsync(leagueId);
        var leaguePlayerDtos =
            leaguePlayers.Select(p => p.ToLeaguePlayerDto()).OrderByDescending(p => p.Score).ToList();

        return Ok(leaguePlayerDtos);
    }

    /// <summary>
    /// Get all games in specific league
    /// </summary>
    /// <response code="200">OK</response>
    /// <response code="400">Bad request</response>
    /// <response code="404">League not found</response>
    [HttpGet("{leagueId}/games")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<GameDto>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAllLeagueGames(int leagueId, [FromQuery] GameQueryObject gameQuery)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await leagueRepository.LeagueExistsAsync(leagueId)) return NotFound();

        var leagueGames = await leagueRepository.GetLeagueGamesAsync(leagueId, gameQuery);
        // var leagueGamesDto = leagueGames.Select(g => g.ToGameDto()).ToList();

        var leagueGamesDto = leagueGames.ToLeagueGamesDto();

        return Ok(leagueGamesDto);
    }

    /// <summary>
    /// Create new league
    /// </summary>
    /// <response code="201">League created</response>
    /// <response code="400">Bad request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="422">League with given name already exists</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(422)]
    public async Task<IActionResult> CreateLeague([FromQuery] CreateLeagueDto leagueCreate)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var leagues = await leagueRepository.GetLeaguesAsync();
        var existingLeague =
            leagues.FirstOrDefault(c => c.Name.Trim().ToUpper().Equals(leagueCreate.Name.TrimEnd().ToUpper()));

        if (existingLeague != null)
        {
            ModelState.AddModelError("", "League already exists");
            return StatusCode(422, ModelState);
        }

        var userEmail = User.GetUserEmail();
        var owner = userManager.FindByEmailAsync(userEmail).Result;
        if (owner == null)
        {
            return BadRequest(ModelState);
        }

        var league = leagueCreate.ToLeagueFromCreateDto(owner);
        await leagueRepository.CreateLeagueAsync(league);

        return Created();
    }

    /// <summary>
    /// Join league
    /// </summary>
    /// <response code="200">OK</response>
    /// <response code="400">Bad request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">League not found</response>
    /// <response code="422">User is already in this league</response>
    [HttpPost("join")]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    [ProducesResponseType(422)]
    public async Task<IActionResult> JoinLeague([FromQuery] [Required] int leagueId)
    {
        var userEmail = User.GetUserEmail();
        var player = userManager.FindByEmailAsync(userEmail).Result;

        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await leagueRepository.LeagueExistsAsync(leagueId))
        {
            return NotFound("League doesnt exist");
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

        return Ok();
    }


    /// <summary>
    /// Leave league
    /// </summary>
    /// <response code="200">OK</response>
    /// <response code="400">Bad request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">League not found</response>
    [HttpDelete("leave")]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> LeaveLeague([FromQuery] [Required] int leagueId)
    {
        var userEmail = User.GetUserEmail();
        var player = userManager.FindByEmailAsync(userEmail).Result;

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var playerLeague = await leagueRepository.RemovePlayerAsync(leagueId, player.Id);

        if (playerLeague == null) return NotFound("This player isn't part of this league");

        return Ok();
    }

    /// <summary>
    /// Delete league
    /// </summary>
    /// <response code="200">OK</response>
    /// <response code="400">Bad request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">League not found</response>
    [HttpDelete]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteLeague([FromQuery] [Required] int leagueId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var league = await leagueRepository.DeleteAsync(leagueId);

        if (league == null) return NotFound("League not found");

        return Ok();
    }

    /// <summary>
    /// Update league info: Name, Description, MaxPlayers, Public, Image
    /// </summary>
    /// <response code="200">OK</response>
    /// <response code="400">Bad request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">League not found</response>
    [HttpPost("{leagueId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateLeague([FromRoute] int leagueId, [FromQuery] UpdateLeagueDto updateLeagueDto,
        IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (!await leagueRepository.LeagueExistsAsync(leagueId))
        {
            return NotFound("League not found");
        }

        if (imageFile == null)
        {
            return BadRequest();
        }

        var image = await imageFile.ToImage();
        await imageRepository.UploadImage(image);
        await leagueRepository.UpdateLeaguePhoto(leagueId, image);
        await leagueRepository.UpdateLeague(leagueId, updateLeagueDto);
        return Ok();
    }
}