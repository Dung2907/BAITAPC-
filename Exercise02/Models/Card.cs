using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class Card
    {
        public Guid Id { get; set; } // UUID for the card

        public Guid CustomerId { get; set; } // Foreign key to Customer

        // Navigation property
        public Customer Customer { get; set; }
    }
}