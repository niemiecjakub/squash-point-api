using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface ITokenService
{
    string CreateToken(Player user);
}