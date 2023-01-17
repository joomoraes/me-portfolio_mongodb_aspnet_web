namespace Web.Application.Domain.Dtos.Views
{
    public class UpdateDtoViews
    {
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string UserIp { get; set; }
        
        private DateTime _updateDate;

        public DateTime updateDate
        {
            get { return _updateDate; }
            set { _updateDate = value; }
        }

    }
}
