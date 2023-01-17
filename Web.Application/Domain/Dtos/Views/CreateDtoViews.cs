namespace Web.Application.Domain.Dtos.Views
{
    public class CreateDtoViews
    {
        public string? Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string UserIp { get; set; }

        private DateTime _createDate;
        public DateTime createDate
        {
            get { return _createDate; }
            set { _createDate = DateTime.UtcNow; }
        }

    }
}
