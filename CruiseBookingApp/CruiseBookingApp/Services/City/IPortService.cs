using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CruiseBookingApp.Services.Port
{
    public interface IPortService
    {
        Task<IEnumerable<Models.Port>> GetPortsAsync();
    }
}