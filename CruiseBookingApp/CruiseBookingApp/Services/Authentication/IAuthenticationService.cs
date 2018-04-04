using System;
using System.Threading.Tasks;
using CruiseBookingApp.Models;

namespace CruiseBookingApp.Services.Authentication
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }

        User AuthenticatedUser { get; }

        Task<bool> LoginAsync(string email, string password);

        Task<bool> UserIsAuthenticatedAndValidAsync();

        Task LogoutAsync();
    }
}
