using Microsoft.EntityFrameworkCore;
using ReserveParkingSpaceApp.Areas.Identity.Data;
using ReserveParkingSpaceApp.Models;

namespace ReserveParkingSpaceApp.Data
{
    public class ParkingSpotContext : DbContext
    {
        public ParkingSpotContext(DbContextOptions<ParkingSpotContext> options) : base(options)
        {

        }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(a => a.ReservedSpot)
                .WithOne(p => p.applicationUser);
        }
    }
}
