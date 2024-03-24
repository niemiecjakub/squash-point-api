using System.ComponentModel.DataAnnotations;

namespace SquashPointAPI.Dto.Point;

public class CreatePointDto
{
    [Required]
    public int SetId { get; set; }
    [Required]
    public string WinnerId { get; set; }
    [Required]
    public string PointType { get; set; }
}