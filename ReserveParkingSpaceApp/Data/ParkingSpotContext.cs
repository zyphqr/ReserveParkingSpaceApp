using Microsoft.EntityFrameworkCore;
using ReserveParkingSpaceApp.Models;

namespace ReserveParkingSpaceApp.Data
{
    public class ParkingSpotContext : DbContext
    {
        public ParkingSpotContext(DbContextOptions<ParkingSpotContext> options) : base(options)
        {

        }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }
    }
}
