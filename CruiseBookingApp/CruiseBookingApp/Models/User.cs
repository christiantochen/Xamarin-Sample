namespace CruiseBookingApp.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Token { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
