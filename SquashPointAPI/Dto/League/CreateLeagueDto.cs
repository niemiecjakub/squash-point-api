using System.ComponentModel.DataAnnotations;

namespace SquashPointAPI.Dto.League;

public class CreateLeagueDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int MaxPlayers { get; set; }
    [Required]
    public bool Public { get; set; }
    public string Description { get; set; }
}