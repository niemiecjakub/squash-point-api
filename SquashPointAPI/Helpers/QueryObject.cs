using SquashPointAPI.Models;

namespace SquashPointAPI.Helpers;

public class QueryObject
{
    public int? LeagueId { get; set; } = null;
    public int? PlayerId { get; set; } = null;
    public string? SortBy { get; set; } = null;
    public bool? isDescending { get; set; } = null;
}