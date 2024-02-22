using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Dto.Account;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager, ITokenService tokenService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var appUser = new AppUser
        {
            UserName = registerDto.Email,
            Email = registerDto.Email
        };

        var createdUser = await userManager.CreateAsync(appUser, registerDto.Password);

        if (createdUser.Succeeded)
        {
            await userManager.AddToRoleAsync(appUser, "User");
            return Ok("User created");
        }

        return StatusCode(500, "Error");
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());

        if (user == null) return Unauthorized("Invalid Email!");

        var result = await signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) return Unauthorized("Account not found and/or password incorrect");

        return Ok(
            new NewUserDto
            {
                Email = user.Email,
                Token = tokenService.CreateToken(user)
            }
        );
    }
}