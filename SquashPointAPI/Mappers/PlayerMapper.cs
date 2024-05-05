using System.Collections;
using System.Diagnostics;
using SquashPointAPI.Dto.Account;
using SquashPointAPI.Dto.Player;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Mappers;

public static class PlayerMapper
{
    public static PlayerDto ToPlayerDto(this Player playerModel)
    {
        return new PlayerDto
        {
            Id = playerModel.Id,
            FullName = $"{playerModel.FirstName} {playerModel.LastName}",
            Photo = playerModel.Photo?.ImageData
        };
    }


    public static PlayerDetailsDto ToPlayerDetailsDto(this Player playerModel, ICollection<Player> followers,
        ICollection<Player> folowees, ICollection<PlayerFriend> friends)
    {
        return new PlayerDetailsDto
        {
            Id = playerModel.Id,
            FullName = $"{playerModel.FirstName} {playerModel.LastName}",
            Sex = playerModel.Sex,
            Email = playerModel.Email,
            Followers = followers.Count(),
            Following = folowees.Count(),
            Friends = friends.Where(pf => pf.Status == 1).Count(),
            Photo = playerModel.Photo?.ImageData
        };
    }


    public static LeaguePlayerDto ToLeaguePlayerDto(this Player playerModel)
    {
        return new LeaguePlayerDto
        {
            Id = playerModel.Id,
            FullName = $"{playerModel.FirstName} {playerModel.LastName}",
            Photo = playerModel.Photo?.ImageData,
            Score = playerModel.PlayerLeagues.First().Score,
            GamesPlayed = playerModel.PlayerGames.Count(pg => pg.Game.Status == "Finished"),
            GamesWon = playerModel.PlayerGames.Count(pg => pg.Game.Winner == playerModel),
            GamesLost = playerModel.PlayerGames.Where(pg => pg.Game.Status == "Finished")
                .Count(pg => pg.Game.Winner != playerModel)
        };
    }

    public static NewUserDto ToNewUserDto(this Player playerModel, string token)
    {
        return new NewUserDto
        {
            Id = playerModel.Id,
            FullName = $"{playerModel.FirstName} {playerModel.LastName}",
            Email = playerModel.Email,
            Token = token,
            Photo = playerModel.Photo?.ImageData
        };
    }

    public static PlayerSocialDto ToPlayerSocialDto(this Player playerModel, ICollection<Player> followers,
        ICollection<Player> followees, ICollection<PlayerFriend> friends)
    {
        return new PlayerSocialDto
        {
            Followers = followers.Select(p => p.ToPlayerDto()).ToList(),
            Following = followees.Select(p => p.ToPlayerDto()).ToList(),
            Friends = friends.Where(pf => pf.Status.Equals(1)).Select(pf =>
                pf.Friend == playerModel ? pf.Player.ToPlayerDto() : pf.Friend.ToPlayerDto()).ToList(),
            SentFriendRequests = friends.Where(pf => pf.Status.Equals(0) && pf.Player == playerModel)
                .Select(pf => pf.Friend.ToPlayerDto()).ToList(),
            ReceivedFriendRequests = friends.Where(pf => pf.Status.Equals(0) && pf.Player != playerModel)
                .Select(pf => pf.Player.ToPlayerDto()).ToList(),
        };
    }
}