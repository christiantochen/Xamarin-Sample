using System;
using System.Collections.Generic;

namespace CruiseBookingApp.Models
{
    public class CruiseGroup : List<Cruise>
    {
        public DateTime DepartureDate { get; set; }
    }
}