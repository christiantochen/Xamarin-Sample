using System;
using System.Collections.Generic;

namespace CruiseBookingApp.Models
{
    public class FirstView
    {
        public List<string> BannerItems { get; set; }
        public List<FirstViewCategory> Categories { get; set; }
    }

    public class FirstViewCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Items { get; set; }
    }
}
