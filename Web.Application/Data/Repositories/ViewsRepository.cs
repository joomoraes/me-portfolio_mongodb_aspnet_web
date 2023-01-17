namespace Web.Application.Data.Repositories
{
    using global::MongoDB.Driver;
    using Web.Application.Data.Schemas;
    using Web.Application.Domain.Dtos.Posts;
    using Web.Application.Domain.Dtos.Views;
    using Web.Application.Domain.Entities;

    public class ViewsRepository
    {
        IMongoCollection<ViewsSchema> _views;

        public ViewsRepository(MongoDB mongoDB)
        {
            _views = mongoDB.DB.GetCollection<ViewsSchema>("views");
        }

        public void Insert(Views views)
        {
            var document = new ViewsSchema
            {
                Id = views.ViewId,
                Longitude = views.Longitude,
                Latitude = views.Longitude,
                datetime = views.datetime,
                UserIp = views.UserIp
            };

            _views.InsertOne(document);
        }

        public async Task<Views> GetLast(string userip)
        {
            var document = _views.AsQueryable().Where(x => x.UserIp == userip).FirstOrDefault();

            return document != null ? new Views(document.Id,
                document.Longitude,
                document.Latitude,
                document.UserIp,
                document.datetime) : new Views();

        }

    }
}
