

using System.Runtime.InteropServices.JavaScript;

namespace SquashPointAPI.Helpers;

public class QueryObject
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
    public string? GameStatus { get; set; } = null;

}