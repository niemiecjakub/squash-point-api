using System.ComponentModel.DataAnnotations;

namespace SquashPointAPI.Dto.Point;

public class CreatePointDto
{
    [Required] public int SetId { get; set; }
    [Required] public string PlayerId { get; set; }
    [Required] public int Points { get; set; }
}