using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CruiseBookingApp.Models;

namespace CruiseBookingApp.Services.Cruise
{
    public class FakeCruiseService : ICruiseService
    {
        static List<CruiseClass> CruiseClasses = new List<CruiseClass>
        {
            new CruiseClass {
                Name = "Economy",
                PricePerAdultMale = 100000,
                PricePerAdultFemale = 70000,
                PricePerBaby = 17000
            },
            new CruiseClass {
                Name = "Business",
                PricePerAdultMale = 125000,
                PricePerAdultFemale = 98000,
                PricePerBaby = 25000
            },
            new CruiseClass {
                Name = "Premium",
                PricePerAdultMale = 200000,
                PricePerAdultFemale = 150000,
                PricePerBaby = 35000
            }
        };

        static CruiseGroup FirstCruises = new CruiseGroup
        {
            new Models.Cruise()
            {
                Id = 1,
                Name = "KM.PANGRANGO",
                Classes = CruiseClasses
            },
            new Models.Cruise()
            {
                Id = 2,
                Name = "KM.PANGRANGO",
                Classes = CruiseClasses
            },
            new Models.Cruise()
            {
                Id = 3,
                Name = "KM.PANGRANGO",
                Classes = CruiseClasses
            }
        };

        static CruiseGroup SecondCruises = new CruiseGroup
        {
            new Models.Cruise()
            {
                Id = 1,
                Name = "KM.KELUD",
                Classes = CruiseClasses
            },
            new Models.Cruise()
            {
                Id = 2,
                Name = "KM.KELUD",
                Classes = CruiseClasses
            },
            new Models.Cruise()
            {
                Id = 3,
                Name = "KM.KELUD",
                Classes = CruiseClasses
            }
        };

        public async Task<IEnumerable<CruiseGroup>> GetCruisesAsync(Models.Port origin,
                                                                      Models.Port destination,
                                                                      DateTime departDate)
        {
            await Task.Delay(1000);

            var r = new Random();

            FirstCruises.DepartureDate = departDate;
            FirstCruises.ForEach((c) =>
            {
                c.DepartureDateTime = FirstCruises.DepartureDate.AddHours(r.Next(1, 23));
                c.ArrivalDateTime = FirstCruises.DepartureDate.AddDays(r.Next(1, 7)).AddHours(r.Next(1, 23));
            });

            SecondCruises.DepartureDate = departDate.AddDays(1);
            SecondCruises.ForEach((c) =>
            {

                c.DepartureDateTime = SecondCruises.DepartureDate.AddHours(r.Next(1, 23));
                c.ArrivalDateTime = SecondCruises.DepartureDate.AddDays(r.Next(1, 7)).AddHours(r.Next(1, 23));
            });

            return new List<CruiseGroup>
            {
                FirstCruises,
                SecondCruises
            };
        }
    }
}
