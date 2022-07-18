using ReserveParkingSpaceApp.Areas.Identity.Data;
using ReserveParkingSpaceApp.Common;
using ReserveParkingSpaceApp.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace ReserveParkingSpaceApp.Models
{
    public class ParkingSpotReservation
    {
        [Key]
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ShiftTypes SpotShift { get; set; }
        public string? TakenbyId { get; set; }
        public ApplicationUser? Takenby { get; set; }

        public int SpotId { get; set; }
        public ParkingSpot Spot {get; set;} 
    }
}
