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
            builder.Entity<ParkingSpot>()
                .HasOne<ApplicationUser>(a => a.Takenby)
                .WithMany(p => p.ReservedSpot);
        }
    }
}
