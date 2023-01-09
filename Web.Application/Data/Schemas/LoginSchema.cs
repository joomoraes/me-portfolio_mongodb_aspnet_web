namespace Web.Application.Data.Schemas
{
    using global::MongoDB.Bson.Serialization.Attributes;
    using global::MongoDB.Bson;
    using Web.Application.Domain.Entities;
    using Web.Application.Domain.Enums;

    public class LoginSchema
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public string UserToken { get; set; }
        public DateTime openSession { get; set; }
        public DateTime expirationSession { get; set; }
        public EStatusLogin status { get; set; }

    }
    public static class LoginSchemaExtension
    {
        public static Login ParseToDomain(this LoginSchema document)
        {
            return new Login(document.UserId, document.UserToken, document.openSession, document.expirationSession, document.status);
        }
    }
}
