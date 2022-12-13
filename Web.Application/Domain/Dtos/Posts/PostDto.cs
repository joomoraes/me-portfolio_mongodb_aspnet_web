namespace Web.Application.Domain.Dtos.Posts
{
    public class PostDto
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        private DateTime _createAt;

        public DateTime CreateAt
        {
            get { return _createAt; }
            set { _createAt = DateTime.UtcNow; }
        }

    }
}
