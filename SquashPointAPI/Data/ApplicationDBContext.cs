using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SquashPointAPI.Models;

namespace SquashPointAPI.Data;

public class ApplicationDBContext : IdentityDbContext<Player>
{
    public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<Player> Players { get; set; }
    public DbSet<League> Leagues { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Set> Set { get; set; }
    public DbSet<Point> Point { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<PlayerLeague> PlayerLeagues { get; set; }
    public DbSet<PlayerGame> PlayerGames { get; set; }
    public DbSet<PlayerFriend> PlayerFriends { get; set; }
    public DbSet<FollowerFollowee> FollowerFollowee { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Player>()
            .HasOne(p => p.Photo)
            .WithMany()
            .HasForeignKey(p => p.PhotoId);

        builder.Entity<League>()
            .HasOne(l => l.Photo)
            .WithMany()
            .HasForeignKey(l => l.PhotoId);
        
        builder.Entity<PlayerLeague>(x => { x.HasKey(pl => new { pl.PlayerId, pl.LeagueId }); });

        builder.Entity<PlayerLeague>()
            .HasOne(pl => pl.Player)
            .WithMany(p => p.PlayerLeagues)
            .HasForeignKey(pl => pl.PlayerId);

        builder.Entity<PlayerLeague>()
            .HasOne(pl => pl.League)
            .WithMany(l => l.PlayerLeagues)
            .HasForeignKey(pl => pl.LeagueId);


        builder.Entity<PlayerGame>(x => { x.HasKey(pg => new { pg.PlayerId, pg.GameId }); });

        builder.Entity<PlayerGame>()
            .HasOne(pg => pg.Player)
            .WithMany(p => p.PlayerGames)
            .HasForeignKey(pg => pg.PlayerId);

        builder.Entity<PlayerGame>()
            .HasOne(pg => pg.Game)
            .WithMany(g => g.PlayerGames)
            .HasForeignKey(pg => pg.GameId);

        builder.Entity<PlayerFriend>(b =>
        {
            b.HasKey(e => new { e.PlayerId, e.FriendId });
            b.HasOne(e => e.Player).WithMany(e => e.Friends);
            b.HasOne(e => e.Friend).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
        });
        
        builder.Entity<FollowerFollowee>(b =>
        {
            b.HasKey(e => new { e.FollowerId, e.FolloweeId });
            b.HasOne(e => e.Follower).WithMany(e => e.Following);
            b.HasOne(e => e.Followee).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
        });
        

        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            },
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }
}