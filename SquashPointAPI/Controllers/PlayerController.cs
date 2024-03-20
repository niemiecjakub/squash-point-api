using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Dto.Game;
using SquashPointAPI.Dto.League;
using SquashPointAPI.Dto.Player;
using SquashPointAPI.Extensions;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController(
    IPlayerRepository playerRepository,
    UserManager<Player> userManager) : Controller
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
        var followers = await playerRepository.GetPlayerFollowersAsync(playerId);
        var folowees = await playerRepository.GetPlayerFolloweesAsync(playerId);
        
        var playerDto = player.ToPlayerDetailsDto(followers, folowees);

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


        var overview = new[]
        {
            new
            {
                name = "Games",
                played = games.Count(),
                won = games.Count(g => g.Winner == player),
                lost = games.Count(g => g.Winner != player)
            },
            new
            {
                name = "Sets",
                played = sets.Count(),
                won = sets.Count(s => s.Winner == player),
                lost = sets.Count(s => s.Winner != player)
            },
            new
            {
                name = "Points",
                played = points.Count(),
                won = points.Count(s => s.Winner == player),
                lost = points.Count(s => s.Winner != player)
            }
        };


        return Ok(overview);
    }

    [HttpGet("{playerId}/friends")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Player>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetPlayerFriends(string playerId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await playerRepository.PlayerExistsAsync(playerId)) return NotFound();

        var friends = await playerRepository.GetPlayerFriendsAsync(playerId);
        var friendDtos = friends.Select(p => p.ToPlayerDto()).ToList();

        //TODO

        return Ok(friendDtos);
    }


    [HttpGet("{playerId}/followers")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetPlayerFollowers(string playerId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!await playerRepository.PlayerExistsAsync(playerId)) return NotFound();

        var followers = await playerRepository.GetPlayerFollowersAsync(playerId);
        var followersDto = followers.Select(f => f.ToPlayerDto()).ToList();

        return Ok(followersDto);
    }
    
    [HttpGet("{playerId}/followees")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetPlayerFollowees(string playerId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!await playerRepository.PlayerExistsAsync(playerId)) return NotFound();

        var followees = await playerRepository.GetPlayerFolloweesAsync(playerId);
        var followeesDto = followees.Select(f => f.ToPlayerDto()).ToList();

        return Ok(followeesDto);
    }

    
    [HttpPost("follow")]
    [Authorize]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> FollowPlayer([FromQuery] string playerId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!await playerRepository.PlayerExistsAsync(playerId)) return NotFound();

        var userEmail = User.GetUserEmail();
        var player = userManager.FindByEmailAsync(userEmail).Result;
        var folowee = await playerRepository.GetPlayerAsync(playerId);

        var playerFollowee = new FollowerFollowee()
        {
            Follower = player,
            Followee = folowee
        };

        await playerRepository.FollowPlayerAsync(playerFollowee);

        return Ok();
    }

    [HttpPost("friend")]
    [Authorize]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddFriend([FromQuery] string playerId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userEmail = User.GetUserEmail();
        var player = userManager.FindByEmailAsync(userEmail).Result;
        var friend = await playerRepository.GetPlayerAsync(playerId);

        //TODO
        return Ok();
    }
}