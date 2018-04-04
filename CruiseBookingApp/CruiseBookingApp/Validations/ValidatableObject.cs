using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace CruiseBookingApp.Validations
{
    public class ValidatableObject<T> : BindableObject, IValidity
    {
        private readonly List<IValidationRule<T>> _validations;
        private readonly List<string> _errors;
        private T _value;
        private bool _isValid;

        public List<IValidationRule<T>> Validations => _validations;

        public List<string> Errors => _errors;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public bool IsValid
        {
            get => _isValid;
            set
            {
                _isValid = value;

                OnPropertyChanged(nameof(Errors));
                OnPropertyChanged();
            }
        }

        public ValidatableObject()
        {
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = _validations.Where(v => !v.Check(Value))
                                                     .Select(v => v.ValidationMessage);

            foreach (var error in errors)
            {
                Errors.Add(error);
            }

            IsValid = !Errors.Any();

            return this.IsValid;
        }
    }
}
