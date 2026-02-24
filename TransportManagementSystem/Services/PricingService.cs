using System.Linq;
using TransportManagementSystem.Data;
using TransportManagementSystem.Models;

namespace TransportManagementSystem.Services
{
    public class PricingService
    {
        private readonly AppDbContext _context;

        public PricingService(AppDbContext context)
        {
            _context = context;
        }

        public decimal CalculateFare(VehicleType vehicleType, double distance)
        {
            var pricing = _context.Pricings
                .FirstOrDefault(p => p.VehicleType == vehicleType);

            if (pricing == null)
                return 0;

            return pricing.BaseFare + (decimal)distance * pricing.RatePerKm;
        }
    }
}
