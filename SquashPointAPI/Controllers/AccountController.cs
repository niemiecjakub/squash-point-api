using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Dto.Account;
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

        var user = await playerRepository.LoginUserAsync(loginDto.Email);
        var result = await signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        
        if (!result.Succeeded) return Unauthorized("Account not found and/or password incorrect");

        string token = tokenService.CreateToken(user);
        var userDto = user.ToNewUserDto(token);

        return Ok(userDto);
    }
}