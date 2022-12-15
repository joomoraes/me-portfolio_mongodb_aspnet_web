namespace Web.Application.Domain.Dtos.Posts
{
    public class CreateDtoPosts
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }


        private DateTime _createAt;

        public DateTime CreateAt
        {
            get { return _createAt; }
            set { _createAt = DateTime.UtcNow; }
        }


    }
}
