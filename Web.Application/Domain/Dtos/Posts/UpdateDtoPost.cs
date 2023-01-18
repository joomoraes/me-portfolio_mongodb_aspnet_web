using System.ComponentModel.DataAnnotations;

namespace Web.Application.Domain.Dtos.Posts
{
    public class UpdateDtoPost
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Text")]
        public string Text { get; set; }
        [Display(Name = "Image")]
        public string Image { get; set; }
        [Display(Name = "Relevance")]
        public int Relevance { get; set; }

        [Display(Name = "Link Image")]
        public string LinkImage { get; set; }

    }
}
