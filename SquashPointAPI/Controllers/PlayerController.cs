﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    IImageRepository imageRepository,
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
        var friends = await playerRepository.GetPlayerFriendsAsync(playerId);

        var playerDto = player.ToPlayerDetailsDto(followers, folowees, friends);

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
        var followee = await playerRepository.GetPlayerAsync(playerId);

        var playerFollowee = new FollowerFollowee()
        {
            Follower = player,
            Followee = followee
        };

        if (await playerRepository.FollowPlayerAsync(playerFollowee))
        {
            return Ok("Ok");
        }

        return BadRequest();
    }

    [HttpDelete("unfollow")]
    [Authorize]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UnfollowPlayer([FromQuery] string playerId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!await playerRepository.PlayerExistsAsync(playerId)) return NotFound();

        var userEmail = User.GetUserEmail();
        var player = userManager.FindByEmailAsync(userEmail).Result;
        var followee = await playerRepository.GetPlayerAsync(playerId);

        if (await playerRepository.UnollowPlayerAsync(player, followee))
        {
            return Ok("Ok");
        }

        return BadRequest("Failed");
    }

    [HttpGet("{playerId}/friends")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Player>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetPlayerFriends(string playerId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await playerRepository.PlayerExistsAsync(playerId)) return NotFound();

        var friends = await playerRepository.GetPlayerFriendsAsync(playerId);
        var playeFriendsDto = friends.Where(pf => pf.Status.Equals(1)).Select(pf => pf.Friend.ToPlayerDto()).ToList();

        return Ok(playeFriendsDto);
    }

    [HttpPost("friend/request")]
    [Authorize]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> RequestFriend([FromQuery] string playerId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!await playerRepository.PlayerExistsAsync(playerId)) return NotFound();

        var userEmail = User.GetUserEmail();
        var player = userManager.FindByEmailAsync(userEmail).Result;
        var friend = await playerRepository.GetPlayerAsync(playerId);

        var playerFriend = new PlayerFriend()
        {
            Player = player,
            Friend = friend,
            Status = 0,
        };

        if (await playerRepository.SendFriendRequestAsync(playerFriend))
        {
            return Ok("Ok");
        }

        return BadRequest();
    }

    [HttpPost("friend/accept")]
    [Authorize]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AcceptFriendRequest([FromQuery] string playerId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!await playerRepository.PlayerExistsAsync(playerId)) return NotFound();

        var userEmail = User.GetUserEmail();
        var player = userManager.FindByEmailAsync(userEmail).Result; //the one who accepts
        var friend = await playerRepository.GetPlayerAsync(playerId); //the one who sent

        if (await playerRepository.AcceptFriendRequestAsync(friend, player, 1))
        {
            return Ok("Ok");
        }

        return BadRequest();
    }

    [HttpDelete("friend/delete")]
    [Authorize]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteFriendRequest([FromQuery] string playerId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!await playerRepository.PlayerExistsAsync(playerId)) return NotFound();

        var userEmail = User.GetUserEmail();
        var player = userManager.FindByEmailAsync(userEmail).Result;
        var friend = await playerRepository.GetPlayerAsync(playerId);

        if (await playerRepository.DeleteFriendAsync(friend, player))
        {
            return Ok("Ok");
        }

        return BadRequest();
    }


    [HttpGet("{playerId}/social")]
    public async Task<IActionResult> SocialData(string playerId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await playerRepository.PlayerExistsAsync(playerId))
        {
            return NotFound();
        }

        var user = await userManager.Users.FirstAsync(u => u.Id == playerId);
        var followers = await playerRepository.GetPlayerFollowersAsync(playerId);
        var followees = await playerRepository.GetPlayerFolloweesAsync(playerId);
        var friends = await playerRepository.GetPlayerFriendsAsync(playerId);

        var playerSocialDto = user.ToPlayerSocialDto(followers, followees, friends);

        return Ok(playerSocialDto);
    }


    [HttpPost("{playerId}/photo")]
    public async Task<IActionResult> UploadPhoto([FromRoute] string playerId, [Required] IFormFile imageFile)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await playerRepository.PlayerExistsAsync(playerId))
        {
            return NotFound();
        }


        // var userEmail = User.GetUserEmail();
        // var user = userManager.FindByEmailAsync(userEmail).Result;

        var image = await imageFile.ToImage();
        await imageRepository.UploadImage(image);
        await playerRepository.UpdatePlayerPhoto(playerId, image);
        return Ok();
    }
}