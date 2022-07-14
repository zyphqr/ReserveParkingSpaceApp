using Microsoft.EntityFrameworkCore;
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
            var spot = _applicationContext.ParkingSpots.Include(x => x.Reservations).FirstOrDefault(spot => spot.Id == spotId);
            if (spot == null)
            {
                throw new Exception("Parking spot does not exist");
            }

            foreach (var reservation in spot.Reservations)
            {
                if ((reservation.StartDate >= startDate && reservation.StartDate <= endDate)
                    || (reservation.EndDate >= startDate && reservation.EndDate <= endDate))
                {
                    if (reservation.SpotShift == spotShift
                        || spotShift == ParkingReservation.Shift.AllDayShift
                        || reservation.SpotShift == ParkingReservation.Shift.AllDayShift)
                    {
                        throw new Exception("The spot is already reserved for that time period");
                    }
                }
            }

            ParkingSpotReservation newReservation = new ParkingSpotReservation();
            newReservation.StartDate = startDate;
            newReservation.EndDate = endDate;
            newReservation.SpotShift = spotShift;
            newReservation.TakenbyId = user.Id;
            newReservation.SpotId = spotId;

            _applicationContext.Add(newReservation);
            _applicationContext.SaveChanges();
        }

        public List<ParkingSpot> GetAllSpotsWithReservations()
        {
            var spots = _applicationContext.ParkingSpots.Include(x => x.Reservations).ToList();
            return spots;
        }

        public int CancelSpotReservation(int spotId, ApplicationUser user)
        {
            int CancelledReservations = 0;
            var spot = _applicationContext.ParkingSpots.Include(x => x.Reservations).FirstOrDefault(spot => spot.Id == spotId);
            if (spot == null)
            {
                throw new Exception("Parking spot does not exist");
            }

            foreach (var reservation in spot.Reservations)
            {
                if(reservation.TakenbyId == user.Id)
                {
                    _applicationContext.Remove(reservation);
                    CancelledReservations++;
                }
            }

            _applicationContext.SaveChanges();
            return CancelledReservations;
        }
    }
}
