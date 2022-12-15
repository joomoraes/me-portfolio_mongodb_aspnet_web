namespace Web.Application.Data.Schemas
{
    using global::MongoDB.Bson;
    using global::MongoDB.Bson.Serialization.Attributes;
    using System.Buffers.Text;
    using System.Runtime.CompilerServices;
    using Web.Application.Domain.ValueObjects;

    public class PostSchema
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public int Relevance { get; set; }
        public DateTime CreateAt { get; set; }

    }

    public static class PostSchemaExtension
    {
        public static Post ParseToDomain(this PostSchema document)
        {
            return new Post(document.Title, document.Text, document.Image, document.Relevance, document.CreateAt, document.UserId);
        }
    }

    
}
