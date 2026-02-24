using System;
using System.Linq;
using TransportManagementSystem.Data;
using TransportManagementSystem.Models;

namespace TransportManagementSystem.Services
{
    public class BookingService
    {
        private readonly AppDbContext _context;
        private readonly PricingService _pricingService;

        public BookingService(AppDbContext context, PricingService pricingService)
        {
            _context = context;
            _pricingService = pricingService;
        }

        public void CreateBooking(int userId, int vehicleId, string source, string destination, double distance)
        {
            var vehicle = _context.Vehicles.FirstOrDefault(v => v.VehicleId == vehicleId);

            if (vehicle == null)
                return;

            var pricing = _context.Pricings
                .FirstOrDefault(p => p.VehicleType == vehicle.VehicleType);

            if (pricing == null)
                return;

            var totalFare = pricing.BaseFare + (pricing.RatePerKm * (decimal)distance);

            var booking = new Booking
            {
                UserId = userId,
                VehicleId = vehicleId,
                Source = source,
                Destination = destination,
                Distance = distance,
                TotalFare = totalFare,
                BookingStatus = BookingStatus.Pending,
                StartTime = null,
                EndTime = null
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }



        public void UpdateBookingStatus(int bookingId, BookingStatus newStatus, int? driverId = null)
        {
            var booking = _context.Bookings.Find(bookingId);
            if (booking == null) return;

            booking.BookingStatus = newStatus;

            if (driverId.HasValue)
                booking.DriverId = driverId;

            if (newStatus == BookingStatus.OnTrip)
                booking.StartTime = DateTime.Now;

            if (newStatus == BookingStatus.Completed)
            {
                booking.EndTime = DateTime.Now;

                var vehicle = _context.Vehicles.Find(booking.VehicleId);
                if (vehicle != null)
                    vehicle.Status = VehicleStatus.Available;
            }

            _context.SaveChanges();
        }
    }
}
