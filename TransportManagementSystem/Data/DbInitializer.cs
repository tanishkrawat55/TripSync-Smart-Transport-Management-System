using System.Linq;
using TransportManagementSystem.Models;
using BCrypt.Net;

namespace TransportManagementSystem.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // Seed Roles
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { RoleName = "Admin" },
                    new Role { RoleName = "Driver" },
                    new Role { RoleName = "Customer" }
                );
                context.SaveChanges();
            }

            // Seed Admin
            if (!context.Users.Any(u => u.Email == "admin@tms.com"))
            {
                var adminRole = context.Roles.FirstOrDefault(r => r.RoleName == "Admin");

                if (adminRole != null)
                {
                    var admin = new User
                    {
                        FullName = "System Admin",
                        Email = "admin@tms.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                        RoleId = adminRole.RoleId
                    };

                    context.Users.Add(admin);
                    context.SaveChanges();
                }
            }

            // Seed Pricing
            if (!context.Pricings.Any())
            {
                context.Pricings.AddRange(
                    new Pricing { VehicleType = VehicleType.SUV, BaseFare = 100, RatePerKm = 18 },
                    new Pricing { VehicleType = VehicleType.Sedan, BaseFare = 70, RatePerKm = 12 },
                    new Pricing { VehicleType = VehicleType.Luxury, BaseFare = 200, RatePerKm = 30 },
                    new Pricing { VehicleType = VehicleType.MPV, BaseFare = 90, RatePerKm = 15 }
                );

                context.SaveChanges();
            }

        }
    }
}
