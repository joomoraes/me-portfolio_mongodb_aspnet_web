
namespace Web.Application.Data.Schemas
{
    using global::MongoDB.Bson;
    using global::MongoDB.Bson.Serialization.Attributes;

    public class PostUserSchema
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }
        public double AverageRelevance { get; set; }

        public List<UserSchema> Users { get; set; }
        public List<PostSchema> Post { get; set; }

    }
}
