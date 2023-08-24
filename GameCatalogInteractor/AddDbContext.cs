using GameCatalogDomain.Entity;
using Microsoft.EntityFrameworkCore;

namespace GameCatalogInteractor;

public class AddDbContext : DbContext
{
    public AddDbContext(DbContextOptions<AddDbContext> options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<AdminReview> AdminReview { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
            .HasOne(g => g.Genre)
            .WithMany(g => g.Games)
            .HasForeignKey(g => g.GenreId);
        
        modelBuilder.Entity<Game>()
            .HasOne(g => g.AdminReview)
            .WithOne(ar => ar.Game)
            .HasForeignKey<AdminReview>(ar => ar.GameId);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Game)
            .WithMany(g => g.Reviews)
            .HasForeignKey(r => r.GameId);
    }
}