using System.ComponentModel.DataAnnotations;

namespace TransportManagementSystem.Models
{
    public class Pricing
    {
        [Key]
        public int PriceId { get; set; }

        public VehicleType VehicleType { get; set; }

        public decimal BaseFare { get; set; }

        public decimal RatePerKm { get; set; }
    }
}
