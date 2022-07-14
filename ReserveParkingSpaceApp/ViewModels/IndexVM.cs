namespace ReserveParkingSpaceApp.ViewModels
{
    public class IndexVM
    {
        public List<ParkingSpotVM> ParkingSpots { get; set; }

        public class ParkingSpotVM
        {
            public int SpotId { get; set; }
            public bool IsReserved { get; set; }    
        }
    }
}
