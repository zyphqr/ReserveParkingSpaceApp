using ReserveParkingSpaceApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace ReserveParkingSpaceApp.Models
{
    public class ParkingSpot
    {
        [Key]
        public int Id { get; set; }
        public bool? IsTaken { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Shift SpotShift { get; set; }
        public ApplicationUser Takenby { get; set; }  
        
        public enum Shift
        {
            FirstShift = 1,
            SecondShift = 2,
            AllDayShift = 3,

        }
    }
}
