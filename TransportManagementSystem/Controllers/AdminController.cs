using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TransportManagementSystem.Data;
using TransportManagementSystem.Models;

namespace TransportManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("UserRole") == "Admin";
        }

        // ================= DASHBOARD =================

        public IActionResult AdminDashboard()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            ViewBag.TotalBookings = _context.Bookings.Count();

            ViewBag.PendingBookings = _context.Bookings
                .Count(b => b.BookingStatus == BookingStatus.Pending);

            ViewBag.CompletedBookings = _context.Bookings
                .Count(b => b.BookingStatus == BookingStatus.Completed);

            ViewBag.TotalDrivers = _context.Users
                .Count(u => u.Role.RoleName == "Driver");

            return View();
        }

        // ================= BOOKING MANAGEMENT =================

        public IActionResult Bookings()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var bookings = _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Vehicle)
                .OrderByDescending(b => b.BookingId)
                .ToList();

            return View(bookings);
        }

        public IActionResult ApproveBooking(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var booking = _context.Bookings
                .FirstOrDefault(b => b.BookingId == id);

            if (booking != null)
            {
                booking.BookingStatus = BookingStatus.Confirmed;

                // Optional: change vehicle status
                var vehicle = _context.Vehicles
                    .FirstOrDefault(v => v.VehicleId == booking.VehicleId);

                if (vehicle != null)
                    vehicle.Status = VehicleStatus.OnTrip;

                _context.SaveChanges();
            }

            return RedirectToAction("Bookings");
        }

        // ================= VEHICLE MANAGEMENT =================

        public IActionResult Vehicles()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            ViewBag.VehicleTypes = Enum.GetValues(typeof(VehicleType))
                .Cast<VehicleType>()
                .Select(v => new SelectListItem
                {
                    Value = ((int)v).ToString(),
                    Text = v.ToString()
                }).ToList();

            var vehicles = _context.Vehicles.ToList();

            return View(vehicles);
        }

        [HttpPost]
        public IActionResult AddVehicle(Vehicle model)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                model.Status = VehicleStatus.Available;
                _context.Vehicles.Add(model);
                _context.SaveChanges();
            }

            return RedirectToAction("Vehicles");
        }
    }
}
