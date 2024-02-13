using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Models;

namespace SquashPointAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<Player> Players { get; set; }
    public DbSet<League> Leagues { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Set> Set { get; set; }
    public DbSet<Point> Point { get; set; }
    public DbSet<PlayerLeague> PlayerLeagues { get; set; }
    public DbSet<PlayerGame> PlayerGames { get; set; }
}