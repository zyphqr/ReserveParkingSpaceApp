using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReserveParkingSpaceApp.Areas.Identity.Data;
using ReserveParkingSpaceApp.Models;

namespace ReserveParkingSpaceApp.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<ParkingSpot> ParkingSpots { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<ParkingSpot>();
        builder.Entity<ParkingSpotReservation>()
              .HasOne<ApplicationUser>(a => a.Takenby)
              .WithMany(p => p.ReservedSpots)
              .HasForeignKey(k => k.TakenbyId);

        builder.Entity<ParkingSpotReservation>()
              .HasOne<ParkingSpot>(a => a.Spot)
              .WithMany(p => p.Reservations)
              .HasForeignKey(k => k.SpotId);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(255);
            builder.Property(u => u.LastName).HasMaxLength(255);
        }
}