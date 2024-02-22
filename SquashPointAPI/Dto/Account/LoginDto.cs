using System.ComponentModel.DataAnnotations;

namespace SquashPointAPI.Dto.Account;

public class LoginDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}