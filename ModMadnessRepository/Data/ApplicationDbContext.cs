using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModMadnessDomain.Domain;
using ModMadnessDomain.Identity;

namespace ModMadnessRepository.Data;

public class ApplicationDbContext : IdentityDbContext<MadnessUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }
    public virtual DbSet<DLC> DLCs { get; set; }
    public virtual DbSet<Mod> Mods { get; set; }
    public virtual DbSet<GameVersion> GameVersions { get; set; }
    public virtual DbSet<Platform> Platforms { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Game>(game =>
        {
            game.HasKey(x => x.Id);
            game.Property(x => x.Title)
                .HasMaxLength(30)
                .IsRequired();
        });
    }
}
