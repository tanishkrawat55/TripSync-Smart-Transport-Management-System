using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementSystem.Data;
using TransportManagementSystem.Models;
using TransportManagementSystem.Models.ViewModels;
using TransportManagementSystem.Services;

namespace TransportManagementSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly DistanceService _distanceService;
        private readonly PricingService _pricingService;
        private readonly BookingService _bookingService;

        public CustomerController(
            AppDbContext context,
            DistanceService distanceService,
            PricingService pricingService,
            BookingService bookingService)
        {
            _context = context;
            _distanceService = distanceService;
            _pricingService = pricingService;
            _bookingService = bookingService;
        }
        [HttpGet]
        public JsonResult GetAllAvailableVehicles()
        {
            var vehicles = _context.Vehicles
                .Where(v => v.Status == VehicleStatus.Available)
                .Select(v => new
                {
                    vehicleId = v.VehicleId,
                    modelName = v.ModelName
                })
                .ToList();

            return Json(vehicles);
        }


        private bool IsCustomer()
        {
            return HttpContext.Session.GetString("UserRole") == "Customer";
        }

        public IActionResult UserDashboard()
        {
            if (!IsCustomer())
                return RedirectToAction("Login", "Account");

            var userId = HttpContext.Session.GetInt32("UserId");

            var bookings = _context.Bookings
                .Include(b => b.Vehicle)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.BookingId)
                .ToList();

            return View(bookings);
        }

        [HttpGet]
        public IActionResult BookRide()
        {
            if (!IsCustomer())
                return RedirectToAction("Login", "Account");

            var viewModel = new BookingViewModel
            {
                VehicleTypes = _context.Pricings
                    .Select(p => new SelectListItem
                    {
                        Value = ((int)p.VehicleType).ToString(),
                        Text = p.VehicleType.ToString() +
                               " (Base: ₹" + p.BaseFare +
                               ", ₹" + p.RatePerKm + "/km)"
                    }).ToList(),

                AvailableVehicles = new List<SelectListItem>()
            };

            return View(viewModel);
        }

        [HttpGet]
        public JsonResult GetVehiclesByType(int vehicleTypeId)
        {
            var vehicles = _context.Vehicles
                .Where(v => (int)v.VehicleType == vehicleTypeId
                            && v.Status == VehicleStatus.Available)
                .Select(v => new
                {
                    v.VehicleId,
                    v.ModelName
                })
                .ToList();

            return Json(vehicles);
        }


        [HttpPost]
        public async Task<IActionResult> GetEstimatedFare(string source, string destination, int vehicleId)
        {
            var vehicle = _context.Vehicles
                .FirstOrDefault(v => v.VehicleId == vehicleId);

            if (vehicle == null)
                return Json(new { error = "Invalid vehicle" });

            var distance = await _distanceService
                .GetDistanceAsync(source, destination);

            var fare = _pricingService
                .CalculateFare(vehicle.VehicleType, distance);

            return Json(new
            {
                distance = distance,
                fare = fare
            });
        }



        [HttpPost]
        public async Task<IActionResult> ConfirmBooking(BookingViewModel model)
        {
            if (!IsCustomer())
                return RedirectToAction("Login", "Account");

            if (model.VehicleId == null)
                return RedirectToAction("BookRide");

            var userId = HttpContext.Session.GetInt32("UserId").Value;

            var distance = await _distanceService
                .GetDistanceAsync(model.Source, model.Destination);

            _bookingService.CreateBooking(
                userId,
                model.VehicleId.Value,
                model.Source,
                model.Destination,
                distance);

            return RedirectToAction("UserDashboard");
        }




    }
}
