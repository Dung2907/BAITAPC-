using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Iso { get; set; }
        public string Name { get; set; }
        public string UpperName { get; set; }
        public string Iso3 { get; set; }
        public short? NumCode { get; set; }
        public int PhoneCode { get; set; }
    }
}