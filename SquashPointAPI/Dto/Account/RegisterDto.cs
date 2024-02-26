using System.ComponentModel.DataAnnotations;

namespace SquashPointAPI.Dto.Account;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Sex { get; set; }
    [Required]
    public string Password { get; set; }
}