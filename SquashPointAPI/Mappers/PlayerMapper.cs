using SquashPointAPI.Dto;
using SquashPointAPI.Dto.League;
using SquashPointAPI.Dto.Player;
using SquashPointAPI.Models;

namespace SquashPointAPI.Mappers;


    public static class PlayerMapper
    {
        public static PlayerDto ToPlayerDto(this Player playerModel)
        {
            return new PlayerDto
            {
                Id = playerModel.Id,
                FirstName = playerModel.FirstName,
                LastName = playerModel.LastName,
                Sex = playerModel.Sex,
                CreatedAt = playerModel.CreatedAt,
            };
        }
        
        public static Player ToPlayerFromCreateDTO(this CreatePlayerDto playerDto)
        {
            return new Player()
            {
                FirstName = playerDto.FirstName,
                LastName = playerDto.LastName,
                Sex = playerDto.Sex,
            };
        }
        
        public static PlayerDetailsDto ToPlayerDetailsDto(this Player playerModel)
        {
            return new PlayerDetailsDto
            {
                Id = playerModel.Id,
                FirstName = playerModel.FirstName,
                LastName = playerModel.LastName,
            };
        }
    }
