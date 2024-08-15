using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class CustomerAddress
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PhoneNumber { get; set; }
        public string DialCode { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public Customer Customer { get; set; }
    }
}