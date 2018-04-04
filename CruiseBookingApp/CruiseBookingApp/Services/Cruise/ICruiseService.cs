using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CruiseBookingApp.Models;

namespace CruiseBookingApp.Services.Cruise
{
    public interface ICruiseService
    {
        Task<IEnumerable<CruiseGroup>> GetCruisesAsync(Models.Port origin, Models.Port destination, DateTime departDate);
    }
}