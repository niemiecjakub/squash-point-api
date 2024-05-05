using SquashPointAPI.Dto.Account;
using SquashPointAPI.Models;

namespace SquashPointAPI.Mappers;

public static class AccountMapper
{
    public static Player ToPlayer(this RegisterDto registerDto)
    {
        return new Player
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Sex = registerDto.Sex
        };
    }
}