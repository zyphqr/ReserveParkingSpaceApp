using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReserveParkingSpaceApp.Areas.Identity.Data;
using ReserveParkingSpaceApp.Common;
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
        [Authorize]
        public IActionResult Index()
        {

            return View();
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
        public IActionResult DateShift(DateShiftVM dateShift)
        {
            var parkingSpots = _parkingSpotService.GetAllSpotsWithReservations();

            var vm = new ParkingSpotsGridVM
            {
                ParkingSpots = parkingSpots.Select(spot => {
                    var reservation = spot.Reservations.Where(reservation => dateShift.Date >= reservation.StartDate
                                                                        && dateShift.Date <= reservation.EndDate
                                                                        && (dateShift.SpotShift == reservation.SpotShift
                                                                        || (dateShift.SpotShift == ShiftTypes.AllDayShift
                                                                        || reservation.SpotShift == ShiftTypes.AllDayShift))).ToList();
                    var userFirstShift =  _userManager.FindByIdAsync(reservation?.FirstOrDefault(firstReservation => firstReservation.SpotShift == ShiftTypes.AllDayShift
                                                                                                 || firstReservation.SpotShift == ShiftTypes.FirstShift)?.TakenbyId).Result;
                    var userSecondShift =  _userManager.FindByIdAsync(reservation?.LastOrDefault(lastReservation => lastReservation.SpotShift == ShiftTypes.AllDayShift
                                                                                                 || lastReservation.SpotShift == ShiftTypes.SecondShift)?.TakenbyId).Result;
                    return new ParkingSpotsGridVM.ParkingSpotVM
                    {
                        SpotId = spot.Id,
                        IsReserved = reservation.Any(),                    
                        ReserverFirstShiftFullName = userFirstShift?.FirstName + " " + userFirstShift?.LastName,
                        ReserverSecondShiftFullName = userSecondShift?.FirstName + " " + userSecondShift?.LastName,

                    };
                }).ToList()
            };
            return PartialView("Views/Home/Partials/_ParkingSpotsGrid.cshtml", vm);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ReserveForm(int spotId, DateTime date, ShiftTypes shift)
        {
            return PartialView("Views/Home/Partials/_ReserveForm.cshtml", new ParkingReservation
            {
                SpotId = spotId,
                StartDate = date,
                EndDate = date, 
                SpotShift = shift,
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ReserveForm(ParkingReservation reservation)
        {
            if (reservation.StartDate < DateTime.Today || reservation.StartDate > reservation.EndDate)
            {
                ModelState.AddModelError("Invalid date", "Start date or end date is invalid");
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

                try
                {
                    _parkingSpotService.ReserveSpot(
                        reservation.SpotId,
                        user,
                        reservation.StartDate,
                        reservation.EndDate,
                        reservation.SpotShift
                   );
                    return Content("<script language='javascript' type='text/javascript'>window.location = '/';</script>");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Error", e.Message);
                }
            }

            return PartialView("Views/Home/Partials/_ReserveForm.cshtml", reservation);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CancelSpotReservation(int spotId)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            int CancelledReservationCount = _parkingSpotService.CancelSpotReservation(spotId, user);

            return Content($"<script language='javascript' type='text/javascript'>alert('{CancelledReservationCount} reservation/s at spot {spotId} successfully cancelled');window.location = '/';</script>");
        }
    }
}