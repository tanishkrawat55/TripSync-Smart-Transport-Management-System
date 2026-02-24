using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TransportManagementSystem.Data;
using TransportManagementSystem.Models;

namespace TransportManagementSystem.Controllers
{
    public class DriverController : Controller
    {
        private readonly AppDbContext _context;

        public DriverController(AppDbContext context)
        {
            _context = context;
        }

        private bool IsDriver()
        {
            return HttpContext.Session.GetString("UserRole") == "Driver";
        }

        public IActionResult DriverDashboard()
        {
            if (!IsDriver())
                return RedirectToAction("Login", "Account");

            var driverId = HttpContext.Session.GetInt32("UserId");

            var trips = _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Vehicle)
                .Where(b => b.DriverId == driverId &&
                            b.BookingStatus != BookingStatus.Completed)
                .ToList();

            return View(trips);
        }
    }
}
