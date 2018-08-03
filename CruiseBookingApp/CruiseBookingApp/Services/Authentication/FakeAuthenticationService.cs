using System;
using System.Threading.Tasks;
using CruiseBookingApp.Models;

namespace CruiseBookingApp.Services.Authentication
{
    public class FakeAuthenticationService : IAuthenticationService
    {
        public bool IsAuthenticated => App.Settings.User != null;

        public User AuthenticatedUser => App.Settings.User;

        public Task<bool> LoginAsync(string email, string password)
        {
            var user = new User
            {
                Email = "john@contoso.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "087779997721"
            };

            App.Settings.User = user;

            return Task.FromResult(true);
        }

        public Task LogoutAsync()
        {
            App.Settings.RemoveUserData();

            return Task.FromResult(true);
        }

        public Task<bool> UserIsAuthenticatedAndValidAsync()
        {
            return Task.FromResult(IsAuthenticated);
        }
    }
}
