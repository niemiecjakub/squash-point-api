using SquashPointAPI.Dto.Player;

namespace SquashPointAPI.Dto.Account;

public class NewUserDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public string FullName { get; set; }
    public byte[]? Photo { get; set; }
}