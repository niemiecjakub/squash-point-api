using System.ComponentModel.DataAnnotations;

namespace SquashPointAPI.Dto.Set;

public class CreateSetDto
{
    [Required]
    public int GameId { get; set; }
    public string? WinnerId { get; set; }
}