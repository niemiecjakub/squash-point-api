using AutoMapper;
using SquashPointAPI.Dto;
using SquashPointAPI.Models;

namespace SquashPointAPI.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Player, PlayerDto>();
    }
}