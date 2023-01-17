namespace Web.Application.Domain.Entities
{
    using FluentValidation;
    using FluentValidation.Results;
    using Microsoft.Extensions.Hosting;
    using System.Data;
    using Web.Application.Data.Schemas;
    using Web.Application.Domain.Enums;
    using Web.Application.Domain.ValueObjects;

    public class Views : AbstractValidator<Views>
    {

        public Views()
        {

        }

        public Views(
            string _ViewId,
            double _Longitude,
            double _Latitude,
            string _UserIp,
            DateTime _datetime)
        {
            ViewId = _ViewId;
            Longitude = _Longitude;
            Latitude = _Latitude;
            UserIp = _UserIp;
            datetime = _datetime;
        }

        public string? ViewId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string UserIp { get; set; }
        public DateTime datetime { get; set; }
        public ValidationResult ValidationResult { get; set; }


        public virtual bool _Validate()
        {
            ValidationResult = Validate(this);

            ValidateLongitude();
            ValidateLatitude();

            return ValidationResult.IsValid;

        }

        private void ValidateLongitude()
        {
            RuleFor(c => c.Longitude)
                .NotEmpty().WithMessage("Longitude cannot be empty");
        }

        private void ValidateLatitude()
        {
            RuleFor(c => c.Latitude)
                .NotEmpty().WithMessage("Latiitude cannot be empty");
        }

    }
}
