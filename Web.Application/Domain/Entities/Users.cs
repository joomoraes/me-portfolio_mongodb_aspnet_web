namespace Web.Application.Domain.Entities
{

    using FluentValidation;
    using FluentValidation.Results;
    using Microsoft.Extensions.Hosting;
    using Web.Application.Data.Schemas;
    using Web.Application.Domain.Enums;
    using Web.Application.Domain.ValueObjects;

    public class Users : AbstractValidator<Users>
    {
        public Users(){}

   
        
        public Users(
            string _Username,
            string _Email,
            DateTime _createat,
            EProfile _Profile)
        {
            Username = _Username;
            Email = _Email;
            Profile = _Profile;
            CreateAt = _createat;
            Posts = new List<Post>();
        } 
        
        public Users(
            string _Id,
            string _Username,
            string _Email,
            string _Password,
            DateTime _createat,
            EProfile _Profile,
            string _Token)
        {
            Id = _Id;
            Username = _Username;
            Email = _Email;
            Password = _Password;
            Profile = _Profile;
            CreateAt = _createat;
            Token = _Token;
            Posts = new List<Post>();
        }  
        
        public Users(
            string _Id,
            string _Username,
            string _Email,
            string _Password,
            DateTime? _updateAt,
            EProfile _Profile)
        {
            Id = _Id;
            Username = _Username;
            Email = _Email;
            Password = _Password;
            Profile = _Profile;
            UpdateAt = _updateAt;
            Posts = new List<Post>();
        }

        public string? Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public EProfile Profile { get; set; }
        public string Token { get; set; }
        public Person Person { get; set; }

        public List<Post> Posts { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public void AtributePerson(Person person)
        {
            Person = person;
        }

        public void PostInsert(Post post)
        {
            Posts.Add(post);
        }

        public virtual bool _Validate()
        {

            ValidationResult = Validate(this);

            ValidateUsername();
            ValidateEmail();
            ValidatePassword();
            ValidatePerson();

            return ValidationResult.IsValid;
        }

        private void ValidateUsername()
        {
            RuleFor(c => c.Username)
                .NotEmpty().WithMessage("Username cannnot be empty")
                .MaximumLength(30).WithMessage("User name cannot be excede 30 characters");
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
        private void ValidatePerson()
        {
            if (Person._Validate())
                return;

            foreach (var erro in Person.ValidationResult.Errors)
                ValidationResult.Errors.Add(erro);
        }

    }
}
