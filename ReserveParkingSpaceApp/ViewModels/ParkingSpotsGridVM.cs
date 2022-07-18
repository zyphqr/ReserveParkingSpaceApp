namespace ReserveParkingSpaceApp.ViewModels
{
    public class ParkingSpotsGridVM
    {
        public List<ParkingSpotVM> ParkingSpots { get; set; }

        public class ParkingSpotVM
        {
            public int SpotId { get; set; }
            public bool IsReserved { get; set; }    

            public string? ReserverFirstShiftFullName { get; set; }

            public string? ReserverSecondShiftFullName { get; set; }
        }
    }
}
