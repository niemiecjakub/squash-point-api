using System.ComponentModel.DataAnnotations;

namespace SquashPointAPI.Dto.Account;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
}