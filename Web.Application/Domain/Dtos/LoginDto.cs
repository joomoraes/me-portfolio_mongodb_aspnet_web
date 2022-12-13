namespace Web.Application.Domain.Dtos
{

    using FluentValidation;
    using FluentValidation.Results;
    using Microsoft.Extensions.Hosting;
    using Web.Application.Data.Schemas;
    using Web.Application.Domain.Enums;
    using Web.Application.Domain.ValueObjects;
    public class LoginDto : AbstractValidator<LoginDto>
    {
        public LoginDto(string _Email,
            string _Password)
        {
            Email = _Email;
            Password = _Password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public virtual bool _Validate()
        {

            ValidationResult = Validate(this);

            ValidateEmail();
            ValidatePassword();

            return ValidationResult.IsValid;
        }

        private void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email address is required")
                .EmailAddress().WithMessage("A valid email is required");
        }

        private void ValidatePassword()
        {
            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Password cannot be empty")
                .MinimumLength(4).WithMessage("Password must be more 4 characters")
                .MaximumLength(30).WithMessage("Password should be max 30 characters");
        }


    }
}
