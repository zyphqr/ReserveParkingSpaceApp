using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReserveParkingSpaceApp.Areas.Identity.Data;
using ReserveParkingSpaceApp.Models;
using ReserveParkingSpaceApp.Services;
using ReserveParkingSpaceApp.ViewModels;
using System.Diagnostics;

namespace ReserveParkingSpaceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ParkingSpotService _parkingSpotService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, 
                                ParkingSpotService parkingSpotSevice,
                                UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _parkingSpotService = parkingSpotSevice;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var parkingspot = _parkingSpotService.GetSpotById(1);
            return View(parkingspot);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ReserveParkingSpace(ParkingReservation reservation)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            _parkingSpotService.ReserveSpot(
                reservation.SpotId,
                user,
                reservation.StartDate,
                reservation.EndDate,
                reservation.SpotShift
           );

            return Ok();
        }
    }
}