using AutoMapper;
using SquashPointAPI.Dto;
using SquashPointAPI.Models;

namespace SquashPointAPI.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Player, PlayerDto>();
        CreateMap<PlayerDto, Player>();
        
        CreateMap<League, LeagueDto>();
        CreateMap<LeagueDto, League>();
        
        CreateMap<Game, GameDto>();
        CreateMap<GameDto, Game>();
        
        CreateMap<Set, SetDto>();
        CreateMap<SetDto, Set>();
        
        CreateMap<Point, PointDto>();
        CreateMap<PointDto, Point>();
    }
}