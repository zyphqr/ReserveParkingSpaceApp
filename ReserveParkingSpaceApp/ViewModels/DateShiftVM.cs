using ReserveParkingSpaceApp.Common;
using System.ComponentModel.DataAnnotations;

namespace ReserveParkingSpaceApp.ViewModels
{
    public class DateShiftVM
    {
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public ShiftTypes SpotShift { get; set; }

    }
}
