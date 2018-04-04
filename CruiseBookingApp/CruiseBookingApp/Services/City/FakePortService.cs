using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CruiseBookingApp.Models;

namespace CruiseBookingApp.Services.Port
{
    public class FakePortService : IPortService
    {
        static List<Models.Port> Ports = new List<Models.Port>()
        {
            new Models.Port {Id = 1,Name = "AGATS"},
            new Models.Port {Id = 2,Name = "AMAHAI"},
            new Models.Port {Id = 3,Name = "AMBON"},
            new Models.Port {Id = 4,Name = "AMPENAN (LEMBAR)"},
            new Models.Port {Id = 5,Name = "AMURANG"},
            new Models.Port {Id = 6,Name = "AWERANGE"},
            new Models.Port {Id = 7,Name = "BABANG (BACAN)"},
            new Models.Port {Id = 8,Name = "BADAS"},
            new Models.Port {Id = 9,Name = "BALIKPAPAN"},
            new Models.Port {Id = 10,Name = "BANDA NAIRA"},
            new Models.Port {Id = 11,Name = "BANGGAI"},
            new Models.Port {Id = 12,Name = "BATU AMPAR (BATAM)"},
            new Models.Port {Id = 13,Name = "BATULICIN"},
            new Models.Port {Id = 14,Name = "BAU-BAU"},
            new Models.Port {Id = 15,Name = "BELAWAN (MEDAN)"},
            new Models.Port {Id = 16,Name = "BELINYU"},
            new Models.Port {Id = 17,Name = "BENOA (DENPASAR)"},
            new Models.Port {Id = 18,Name = "BIAK"},
            new Models.Port {Id = 19,Name = "BITUNG"},
            new Models.Port {Id = 20,Name = "BOTANG"},
            new Models.Port {Id = 21,Name = "BULA"},
            new Models.Port {Id = 22,Name = "BULI"},
            new Models.Port {Id = 23,Name = "BUOL"},
            new Models.Port {Id = 24,Name = "DOBO"},
            new Models.Port {Id = 25,Name = "ENDE"},
        };

        public async Task<IEnumerable<Models.Port>> GetPortsAsync()
        {
            await Task.Delay(1000);

            return Ports;
        }
    }
}
