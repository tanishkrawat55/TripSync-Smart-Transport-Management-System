using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TransportManagementSystem.Data;
using TransportManagementSystem.Models;
using TransportManagementSystem.Models.ViewModels;
using BCrypt.Net;

namespace TransportManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // ================= LOGIN =================

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Email == model.Email);

                if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                {
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    HttpContext.Session.SetString("UserRole", user.Role.RoleName);
                    HttpContext.Session.SetString("UserName", user.FullName);

                    if (user.Role.RoleName == "Admin")
                        return RedirectToAction("AdminDashboard", "Admin");

                    if (user.Role.RoleName == "Driver")
                        return RedirectToAction("DriverDashboard", "Driver");

                    if (user.Role.RoleName == "Customer")
                        return RedirectToAction("UserDashboard", "Customer");
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model);
        }

        // ================= REGISTER =================

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email already registered.");
                    return View(model);
                }

                var customerRole = _context.Roles
                    .FirstOrDefault(r => r.RoleName == "Customer");

                if (customerRole == null)
                {
                    ModelState.AddModelError("", "Registration not available.");
                    return View(model);
                }

                var user = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    RoleId = customerRole.RoleId
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(model);
        }

        // ================= LOGOUT =================

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
