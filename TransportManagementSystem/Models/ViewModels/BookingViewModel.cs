using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TransportManagementSystem.Models.ViewModels
{
    public class BookingViewModel
    {
        public string Source { get; set; }
        public string Destination { get; set; }

        public int VehicleTypeId { get; set; }

        public int? VehicleId { get; set; }

        public IEnumerable<SelectListItem> VehicleTypes { get; set; }

        public List<SelectListItem> AvailableVehicles { get; set; }

        public double Distance { get; set; }

        public decimal EstimatedFare { get; set; }
    }
}
