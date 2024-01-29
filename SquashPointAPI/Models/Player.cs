namespace SquashPointAPI.Models;

public class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Sex sex { get; set; }
    public ICollection<PlayerLeague> PlayerLeagues;
    public ICollection<Game> Games;
}