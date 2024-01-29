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
    public DbSet<PlayerLeague> PlayerLeagues { get; set; }
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<PlayerLeague>()
    //         .HasKey(pl => new { pl.PlayerId, pl.LeagueId });
    //     modelBuilder.Entity<PlayerLeague>()
    //         .HasOne(pl => pl.Player)
    //         .WithMany(p => p.PlayerLeagues)
    //         .HasForeignKey(pl => pl.PlayerId);
    //     modelBuilder.Entity<PlayerLeague>()
    //         .HasOne(pl => pl.League)
    //         .WithMany(l => l.PlayerLeagues)
    //         .HasForeignKey(pl => pl.LeagueId);
    //     
    // }
}