using System;
using System.Collections.Generic;

namespace CruiseBookingApp.Models
{
    public class Cruise
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }

        public List<CruiseClass> Classes { get; set; }
        public List<string> Ports { get; set; }
    }

    public class CruiseClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double PricePerAdultMale { get; set; }
        public double PricePerAdultFemale { get; set; }
        public double PricePerBaby { get; set; }
    }
}
