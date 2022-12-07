using FluentValidation;

namespace Web.Application.Controllers.Inputs
{
    public class IncludePosts 
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public int Relevance { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
