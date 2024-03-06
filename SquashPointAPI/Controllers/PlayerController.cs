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
    [HttpGet("all")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Player>))]
    public async Task<IActionResult> GetPlayers()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var players = await playerRepository.GetPlayersAsync();
        var playerDtos = players.Select(s => s.ToPlayerDto()).ToList();

        return Ok(playerDtos);
    }

    [HttpGet("{playerId}")]
    [ProducesResponseType(200, Type = typeof(Player))]
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
    [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
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
    [ProducesResponseType(200, Type = typeof(IEnumerable<League>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetPlayerLeagues(string playerId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await playerRepository.PlayerExistsAsync(playerId)) return NotFound();

        var leagues = await playerRepository.GetPlayerLeagues(playerId);
        var leagueDtos = leagues.Select(l => l.ToLeagueDto()).ToList();

        return Ok(leagueDtos);
    }
}