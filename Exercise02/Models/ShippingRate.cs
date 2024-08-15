using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class ShippingRate
    {
        public Guid Id { get; set; } // UUID for the shipping rate

        public int ShippingZoneId { get; set; } // Foreign key to ShippingZone

        public string WeightUnit { get; set; } // Weight unit ('g' or 'kg')

        public decimal MinValue { get; set; } // Minimum value for shipping rates

        public decimal? MaxValue { get; set; } // Maximum value for shipping rates

        public bool NoMax { get; set; } = true; // Indicator if there's no maximum value

        public decimal Price { get; set; } // Cost associated with the shipping rate

        // Navigation properties
        public ShippingZone ShippingZone { get; set; }
    }
}