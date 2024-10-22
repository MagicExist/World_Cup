using Microsoft.EntityFrameworkCore;

namespace World_Cup.DataBase;

public partial class WorldCupsDbContext : DbContext
{
    public WorldCupsDbContext()
    {
    }

    public WorldCupsDbContext(DbContextOptions<WorldCupsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChampionShip> ChampionShips { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<CountriesByGroup> CountriesByGroups { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Phase> Phases { get; set; }

    public virtual DbSet<Stadium> Stadiums { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChampionShip>(entity =>
        {
            entity.HasIndex(e => e.CountryId, "IX_ChampionShips_CountryId");

            entity.Property(e => e.ChampionShip1)
                .HasMaxLength(100)
                .HasColumnName("ChampionShip");

            entity.HasOne(d => d.Country).WithMany(p => p.ChampionShips).HasForeignKey(d => d.CountryId);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasIndex(e => e.CountryId, "IX_Cities_CountryId");

            entity.Property(e => e.City1)
                .HasMaxLength(100)
                .HasColumnName("City");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities).HasForeignKey(d => d.CountryId);
        });

        modelBuilder.Entity<CountriesByGroup>(entity =>
        {
            entity.HasIndex(e => e.CountryId, "IX_CountriesByGroups_CountryId");

            entity.HasIndex(e => e.GroupId, "IX_CountriesByGroups_GroupId");

            entity.HasOne(d => d.Country).WithMany(p => p.CountriesByGroups)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Group).WithMany(p => p.CountriesByGroups)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Country1)
                .HasMaxLength(75)
                .HasColumnName("Country");
            entity.Property(e => e.Entity).HasMaxLength(100);
            entity.Property(e => e.EntityLogo).HasMaxLength(2048);
            entity.Property(e => e.Flag).HasMaxLength(2048);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasIndex(e => e.ChampionShipId, "IX_Groups_ChampionShipId");

            entity.Property(e => e.Group1)
                .HasMaxLength(1)
                .HasColumnName("Group");

            entity.HasOne(d => d.ChampionShip).WithMany(p => p.Groups).HasForeignKey(d => d.ChampionShipId);
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasIndex(e => e.ChampionShipId, "IX_Matches_ChampionShipId");

            entity.HasIndex(e => e.FirstCountryId, "IX_Matches_FirstCountryId");

            entity.HasIndex(e => e.PhaseId, "IX_Matches_PhaseId");

            entity.HasIndex(e => e.SecondCountryId, "IX_Matches_SecondCountryId");

            entity.HasIndex(e => e.StadiumId, "IX_Matches_StadiumId");

            entity.HasOne(d => d.ChampionShip).WithMany(p => p.Matches).HasForeignKey(d => d.ChampionShipId);

            entity.HasOne(d => d.FirstCountry).WithMany(p => p.MatchFirstCountries)
                .HasForeignKey(d => d.FirstCountryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Phase).WithMany(p => p.Matches)
                .HasForeignKey(d => d.PhaseId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SecondCountry).WithMany(p => p.MatchSecondCountries)
                .HasForeignKey(d => d.SecondCountryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Stadium).WithMany(p => p.Matches)
                .HasForeignKey(d => d.StadiumId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Phase>(entity =>
        {
            entity.Property(e => e.Phase1)
                .HasMaxLength(50)
                .HasColumnName("Phase");
        });

        modelBuilder.Entity<Stadium>(entity =>
        {
            entity.HasIndex(e => e.CityId, "IX_Stadiums_CityId");

            entity.Property(e => e.Photo).HasMaxLength(2048);
            entity.Property(e => e.Stadium1)
                .HasMaxLength(100)
                .HasColumnName("Stadium");

            entity.HasOne(d => d.City).WithMany(p => p.Stadia).HasForeignKey(d => d.CityId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
