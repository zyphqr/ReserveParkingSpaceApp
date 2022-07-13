using ReserveParkingSpaceApp.Areas.Identity.Data;
using ReserveParkingSpaceApp.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace ReserveParkingSpaceApp.Models
{
    public class ParkingSpot
    {
        [Key]
        public int Id { get; set; }
        public bool IsTaken { get; set; }
        
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ParkingReservation.Shift? SpotShift { get; set; }
        public string? TakenbyId { get; set; }  
        public ApplicationUser? Takenby { get; set; }  
        
       
    }
}
