using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using CruiseBookingApp.Services.Authentication;
using CruiseBookingApp.Validations;
using CruiseBookingApp.ViewModels.Base;

namespace CruiseBookingApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        ValidatableObject<string> _userName;
        ValidatableObject<string> _password;

        readonly IAuthenticationService _authenticationService;

        public LoginViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            AddValidations();
        }

        public ValidatableObject<string> UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public ValidatableObject<string> Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Username should not be empty" });
            _userName.Validations.Add(new EmailRule());
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password should not be empty" });
        }

        bool ValidateAll()
        {
            var usernameIsValid = _userName.Validate();
            var passwordIsValid = _password.Validate();

            return usernameIsValid && passwordIsValid;
        }

        public ICommand LogInCommand => new AsyncCommand(LogInAsync);
        public ICommand ValidateCommand => new Command(Validate);

        async Task LogInAsync()
        {
            if (!ValidateAll())
                return;

            IsBusy = true;

            try
            {
                bool isAuth = await _authenticationService.LoginAsync(UserName.Value, Password.Value);

                if (isAuth)
                    await NavigationService.NavigateToAsync<MainViewModel>();
                else
                    DialogService.ShowToast("Username or Password incorrect");
            }
            catch (Exception ex)
            {
                DialogService.ShowToast(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        void Validate(object obj)
        {
            if (obj == _userName)
            {
                _userName.Validate();
            }
            else if (obj == _password)
            {
                _password.Validate();
            }
        }
    }
}
