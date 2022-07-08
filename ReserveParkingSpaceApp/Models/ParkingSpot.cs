using System.ComponentModel.DataAnnotations;

namespace ReserveParkingSpaceApp.Models
{
    public class ParkingSpot
    {
        [Key]
        public int Id { get; set; }
        public bool IsTaken { get; set; }
        public string TakenBy { get; set; }
    }
}
