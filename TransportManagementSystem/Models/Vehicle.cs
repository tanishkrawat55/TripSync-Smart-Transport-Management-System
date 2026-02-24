using System.ComponentModel.DataAnnotations;

namespace TransportManagementSystem.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }

        public string ModelName { get; set; }

        public string PlateNumber { get; set; }

        public VehicleType VehicleType { get; set; }

        public VehicleStatus Status { get; set; }
    }

    public enum VehicleType
    {
        SUV = 0,
        Sedan = 1,
        Luxury = 2,
        MPV = 3
    }


    public enum VehicleStatus
    {
        Available,
        OnTrip,
        Maintenance
    }
}
