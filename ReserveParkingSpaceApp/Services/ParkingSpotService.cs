using Microsoft.EntityFrameworkCore;
using ReserveParkingSpaceApp.Areas.Identity.Data;
using ReserveParkingSpaceApp.Common;
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
        public void ReserveSpot(int spotId, ApplicationUser user, DateTime startDate, DateTime endDate, ShiftTypes spotShift)
        {
            var spot = _applicationContext.ParkingSpots.Include(x => x.Reservations).FirstOrDefault(spot => spot.Id == spotId);
            if (spot == null)
            {
                throw new Exception("Parking spot does not exist");
            }
            TimeSpan durationCheck = endDate - startDate;
            TimeSpan inAdvanceCheck = startDate - DateTime.Today;
            if (durationCheck.Days > 7)
            {
                throw new Exception("Spot cannot be reserved for more than 7 days.");
            }
            else if (inAdvanceCheck.Days >= System.DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month))
            {
                throw new Exception("Spot cannot be reserved more than a month in advance.");
            }

            foreach (var reservation in spot.Reservations)
            {
                if ((reservation.StartDate >= startDate && reservation.StartDate <= endDate)
                    || (reservation.EndDate >= startDate && reservation.EndDate <= endDate))
                {
                    if (reservation.SpotShift == spotShift
                        || spotShift == ShiftTypes.AllDayShift
                        || reservation.SpotShift == ShiftTypes.AllDayShift)
                    {
                        throw new Exception("The spot is already reserved for that time period.");
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
