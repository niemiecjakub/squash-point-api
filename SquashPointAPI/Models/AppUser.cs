using Microsoft.AspNetCore.Identity;

namespace SquashPointAPI.Models;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Sex { get; set; }
}