using ReserveParkingSpaceApp.Areas.Identity.Data;
using ReserveParkingSpaceApp.Models;
using ReserveParkingSpaceApp.ViewModels;

namespace ReserveParkingSpaceApp.Services
{
    public class ParkingSpotService
    {
        private readonly ApplicationDbContext _applicationContext;

        public ParkingSpotService(ApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public ParkingSpot GetSpotById(int spotId)
        {
            var spot = _applicationContext.Set<ParkingSpot>().FirstOrDefault(spot => spot.Id == spotId);
            if (spot == null)
            {
                throw new Exception("Parking spot does not exist");
            }
            return spot;
        }
        public void ReserveSpot(int spotId, ApplicationUser user, DateTime startDate, DateTime endDate, ParkingReservation.Shift spotShift)
        {
            var spot = _applicationContext.Set<ParkingSpot>().FirstOrDefault(spot => spot.Id == spotId);
            if(spot == null)
            {
                throw new Exception("Parking spot does not exist");
            }

            spot.StartDate = startDate;
            spot.EndDate = endDate;
            spot.SpotShift = spotShift;
            spot.TakenbyId = user.Id;
            spot.IsTaken = true;

            _applicationContext.Update(spot);
            _applicationContext.SaveChanges();
        }
    }
}
