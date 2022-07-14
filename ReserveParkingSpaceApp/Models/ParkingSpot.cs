using ReserveParkingSpaceApp.Areas.Identity.Data;
using ReserveParkingSpaceApp.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReserveParkingSpaceApp.Models
{
    public class ParkingSpot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public ICollection<ParkingSpotReservation> Reservations { get; set; }
    }
}
