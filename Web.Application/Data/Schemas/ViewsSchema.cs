namespace Web.Application.Data.Schemas
{
    using global::MongoDB.Bson;
    using global::MongoDB.Bson.Serialization.Attributes;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System.Runtime.CompilerServices;
    using Web.Application.Domain.Entities;

    public class ViewsSchema
    {

        [BsonRepresentation(BsonType.ObjectId)]
        public string  Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string UserIp { get; set; }

        private DateTime _datetime;

        public DateTime datetime
        {
            get { return _datetime; }
            set { _datetime = DateTime.UtcNow; }
        }



    }

    public static class ViewsSchemaExtension
    {
        public static Views ParseToDomain(this ViewsSchema document)
        {
            var views = new Views(
                document.Id,
                document.Longitude,
                document.Latitude,
                document.UserIp,
                document.datetime);

            return views;
            }

    }
}
