
namespace Web.Application.Domain.ValueObjects
{
using FluentValidation;
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;

    public class Person : AbstractValidator<Person>
    {
        public Person(
            string? _ZipCode, 
            string? _City, 
            string? _State,
            string? _Country)
        {
            ZipCode = _ZipCode;
            City = _City;
            State = _State;
            Country = _Country;
        }

        public string? Biography { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public double? TotalSponser { get; set; }
        public string? Medals { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public virtual bool _Validate()
        {
            ValidateZipCode();
            ValidateCity();
            ValidateState();
            ValidateCountry();

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
        private void ValidateZipCode()
        {
            RuleFor(c => c.ZipCode)
                .NotEmpty().WithMessage("ZipCode cannot be emmpty")
                .MaximumLength(10).WithMessage("ZipCode cannot excede 10 characteres");
        }
        private void ValidateCity()
        {
            RuleFor(c => c.City)
                .NotEmpty().WithMessage("City cannnot be empty")
                .MaximumLength(50).WithMessage("City cannot excede 10 characteres");
        }
        private void ValidateState()
        {
            RuleFor(c => c.State)
                .NotEmpty().WithMessage("State cannot be empty")
                .MaximumLength(4).WithMessage("State cannot excede 4 chacteres");
        }
        private void ValidateCountry()
        {
            RuleFor(c => c.Country)
                .NotEmpty().WithMessage("Country cannot be empty")
                .MaximumLength(20).WithMessage("Country cannnot exceded 20 characteres");
        }

    }
}
