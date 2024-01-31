namespace SquashPointAPI.Models;

public class Player
{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public ICollection<PlayerLeague> PlayerLeagues;
        public ICollection<PlayerGame> PlayerGames;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}