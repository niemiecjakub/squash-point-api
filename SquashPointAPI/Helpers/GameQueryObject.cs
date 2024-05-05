namespace SquashPointAPI.Helpers;

public class GameQueryObject
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
    public string? GameStatus { get; set; } = null;
    public int? WinnerId { get; set; } = null;
    public bool OrderByScheduledDate { get; set; } = false;
    public bool OrderByCreateDate { get; set; } = true;
}