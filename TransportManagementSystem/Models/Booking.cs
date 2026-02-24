using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportManagementSystem.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        // Customer
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        // Vehicle
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        // Driver (nullable initially)
        [ForeignKey("Driver")]
        public int? DriverId { get; set; }
        public User Driver { get; set; }

        public string Source { get; set; }
        public string Destination { get; set; }

        public double Distance { get; set; }  // in KM

        public decimal TotalFare { get; set; }

        public BookingStatus BookingStatus { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Accepted,
        OnTrip,
        Completed
    }
}
