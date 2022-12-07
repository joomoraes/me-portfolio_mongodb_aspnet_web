
namespace Web.Application.Domain.ValueObjects
{

    using FluentValidation;
    using FluentValidation.Results;

    public class Post : AbstractValidator<Post>
    {

        public Post(
         string id,
         string title,
         string text,
         string image,
         DateTime createat)
        {
            Id = id;
            Title = title;
            Text = text;
            Image = image;
            CreateAt = createat;
        }

        public Post(
            string title,
            string text,
            string image,
            int relevance,
            DateTime createat,
            string userid)
        {
            UserId = userid;
            Title = title;
            Text = text;
            Image = image;
            Relevance = relevance;
            CreateAt = createat;
        }

        public Post(
           string id,
           string title,
           string text,
           string image,
           int relevance,
           DateTime createat)
        {
            Id = id;
            Title = title;
            Text = text;
            Image = image;
            Relevance = relevance;
            CreateAt = createat;
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public int Relevance { get; set; }
        public DateTime CreateAt { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public virtual bool _Validate()
        {
            ValidateTitle();
            ValidateText();

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }

        private void ValidateTitle()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Title cannot empty")
                .MaximumLength(100).WithMessage("Title cannot excede 100 characteres limits");
        }

        private void ValidateText()
        {
            RuleFor(c => c.Title)
                .MaximumLength(5).WithMessage("Text cannot exxcede 5 characteres limits");
        }

        private void ValidateRelevance()
        {
            RuleFor(c => c.Relevance)
                .GreaterThan(0).WithMessage("Number of relevance most begone then zero")
                .LessThanOrEqualTo(5).WithMessage("Number of relevance most begone less or equal five");
        }
        
    }
}
