using System.ComponentModel.DataAnnotations;

namespace ReserveParkingSpaceApp.Common
{
    public enum ShiftTypes
    {
        [Display(Name = "First shift")]
        FirstShift = 1,
        [Display(Name = "Second shift")]
        SecondShift = 2,
        [Display(Name = "All day shift")]
        AllDayShift = 3,

    }
}
