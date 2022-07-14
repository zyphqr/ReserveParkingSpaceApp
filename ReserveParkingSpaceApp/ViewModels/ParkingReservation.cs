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
        public Shift SpotShift { get; set; }

        public enum Shift
        {
            [Display(Name = "First shift")]
            FirstShift = 1,
            [Display(Name = "Second shift")]
            SecondShift = 2,
            [Display(Name = "All day shift")]
            AllDayShift = 3,

        }
    }
}
