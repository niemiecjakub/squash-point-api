using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto.Game;
using SquashPointAPI.Dto.League;
using SquashPointAPI.Dto.Player;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController(
    IPlayerRepository playerRepository) : Controller
{
    [HttpGet("all")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<PlayerDto>))]
    public async Task<IActionResult> GetPlayers()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var players = await playerRepository.GetPlayersAsync();
        var playerDtos = players.Select(s => s.ToPlayerDto()).ToList();

        return Ok(playerDtos);
    }

    [HttpGet("{playerId}")]
    [ProducesResponseType(200, Type = typeof(PlayerDetailsDto))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetPlayerById(string playerId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await playerRepository.PlayerExistsAsync(playerId)) return NotFound();
        var player = await playerRepository.GetPlayerAsync(playerId);
        var playerDto = player.ToPlayerDetailsDto();

        return Ok(playerDto);
    }

    [HttpGet("{playerId}/games")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<GameDto>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetPlayerGames(string playerId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await playerRepository.PlayerExistsAsync(playerId)) return NotFound();

        var games = await playerRepository.GetPlayerGamesAsync(playerId);
        var gameDtos = games.Select(g => g.ToGameDto()).ToList();

        return Ok(gameDtos);
    }

    [HttpGet("{playerId}/leagues")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<LeagueDto>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetPlayerLeagues(string playerId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await playerRepository.PlayerExistsAsync(playerId)) return NotFound();

        var leagues = await playerRepository.GetPlayerLeagues(playerId);
        var leagueDtos = leagues.Select(l => l.ToLeagueDto()).ToList();

        return Ok(leagueDtos);
    }

    [HttpGet("{playerId}/games/overview")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<League>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetPlayerGamesOverview(string playerId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await playerRepository.PlayerExistsAsync(playerId)) return NotFound();

        var player = await playerRepository.GetPlayerAsync(playerId);
        var games = await playerRepository.GetPlayerFinishedGamesAsync(playerId);
        var sets = games.SelectMany(g => g.Sets);
        var points = sets.SelectMany(s => s.Points);

        var overview = new
        {
            Games = new
            {
                played = games.Count(),
                won = games.Count(g => g.Winner == player),
                lost = games.Count(g => g.Winner != player)
            },
            Sets = new
            {
                played = sets.Count(),
                won = sets.Count(s => s.Winner == player),
                lost = sets.Count(s => s.Winner != player)
            },
            Points = new
            {
                played = points.Count(),
                won = points.Count(s => s.Winner == player),
                lost = points.Count(s => s.Winner != player)
            }
        };

        return Ok(overview);
    }
}