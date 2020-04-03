using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.Models
{
    public class TruckLocation
    {
        public string STREET { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string ZIP { get; set; }
        public DateTime START { get; set; }
        public DateTime STOP { get; set; }

    }
}
