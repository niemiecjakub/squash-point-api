namespace SquashPointAPI.Models;

public class Player
{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public ICollection<PlayerLeague> PlayerLeagues { get; set; }
        public ICollection<PlayerGame> PlayerGames { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}