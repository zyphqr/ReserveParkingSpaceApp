using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ReserveParkingSpaceApp.Models;

namespace ReserveParkingSpaceApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
[Table("AspNetUsers")]
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }    
    public Department UserDepartment { get; set; }
    public ICollection<ParkingSpotReservation> ReservedSpots { get; set; }

    public enum Department
    {
        [Display(Name = "IT")]
        IT = 1,
        [Display(Name = "Engineering")]
        Engineering = 2,
        [Display(Name = "Human Resources")]
        HumanResources = 3,
    }
}

