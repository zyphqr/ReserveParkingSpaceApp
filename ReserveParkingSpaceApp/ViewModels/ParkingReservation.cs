using ReserveParkingSpaceApp.Common;
using ReserveParkingSpaceApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ReserveParkingSpaceApp.ViewModels
{
    public class ParkingReservation
    {
        [Required]
        public int SpotId { get; set; }

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);

        [Required]
        public ShiftTypes SpotShift { get; set; }

        public IFormFile? File { get; set; }

    }
}
