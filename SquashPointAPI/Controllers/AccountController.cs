using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Dto.Account;
using SquashPointAPI.Extensions;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Mappers;
using SquashPointAPI.Models;

namespace SquashPointAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AccountController(
    UserManager<Player> userManager,
    IPlayerRepository playerRepository,
    SignInManager<Player> signinManager,
    IImageRepository imageRepository,
    IAccountRepository accountRepository,
    ITokenService tokenService) : ControllerBase
{
    /// <summary>
    /// Registers new user
    /// </summary>
    /// <param name="registerDto"></param>
    /// <returns>Newly created user</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Account/register
    ///     {
    ///        "email": email@email.com,
    ///        "firstName": "XYZ",
    ///        "lastName": "XYZ",
    ///        "sex": "Male",
    ///        "password": "SecretPassword1!"
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">OK</response>
    /// <response code="500">If error occured</response>
    [HttpPost("register")]
    [ProducesResponseType(200, Type = typeof(NewUserDto))]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = registerDto.ToPlayer();

            var createdUser = await userManager.CreateAsync(user, registerDto.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(user, "User");
                if (roleResult.Succeeded)
                {
                    string token = tokenService.CreateToken(user);
                    return Ok(user.ToNewUserDto(token));
                }

                return StatusCode(500, roleResult.Errors);
            }

            return StatusCode(500, createdUser.Errors);
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }


    /// <summary>
    /// Logs in registered user
    /// </summary>
    /// <param name="loginDto"></param>
    /// <returns>User with token</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Account/login
    ///     {
    ///        "email": email@email.com,
    ///        "password": "SecretPassword1!"
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">OK</response>
    /// <response code="401">Unauthorized</response>
    [HttpPost("login")]
    [ProducesResponseType(200, Type = typeof(NewUserDto))]
    [ProducesResponseType(401)]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await accountRepository.LoginUserAsync(loginDto.Email);
        var result = await signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) return Unauthorized("Account not found and/or password incorrect");

        string token = tokenService.CreateToken(user);
        var userDto = user.ToNewUserDto(token);

        return Ok(userDto);
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

        if (await accountRepository.FollowPlayerAsync(playerFollowee))
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

        if (await accountRepository.UnollowPlayerAsync(player, followee))
        {
            return Ok("Ok");
        }

        return BadRequest("Failed");
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

        if (await accountRepository.SendFriendRequestAsync(playerFriend))
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

        if (await accountRepository.AcceptFriendRequestAsync(friend, player, 1))
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

        if (await accountRepository.DeleteFriendAsync(friend, player))
        {
            return Ok("Ok");
        }

        return BadRequest();
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
        await accountRepository.UpdatePlayerPhoto(playerId, image);
        return Ok();
    }
    
    //TODO
    [HttpGet]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UserInfo()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // var userEmail = User.GetUserEmail();
        // var player = userManager.FindByEmailAsync(userEmail).Result;
        
        string playerId = "488f6a43-4a1b-43d8-a5c8-524f73b0559e";
        var player = await playerRepository.GetPlayerAsync(playerId);
        var followers = await playerRepository.GetPlayerFollowersAsync(playerId);
        var followees = await playerRepository.GetPlayerFolloweesAsync(playerId);
        var friends = await playerRepository.GetPlayerFriendsAsync(playerId);
        
        return Ok(player.ToPlayerDetailsDto(followers, followees, friends));
    }
}