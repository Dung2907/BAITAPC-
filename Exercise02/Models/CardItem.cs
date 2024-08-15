using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class CardItem
    {
        public Guid Id { get; set; } // UUID for the card item

        public Guid CardId { get; set; } // Foreign key to Card

        public Guid ProductId { get; set; } // Foreign key to Product

        public int Quantity { get; set; } = 1; // Quantity, defaulting to 1

        // Navigation properties
        public Card Card { get; set; }
        public Product Product { get; set; }
    }
}