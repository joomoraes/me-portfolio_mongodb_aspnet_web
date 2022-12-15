
namespace Web.Application.Data.Schemas
{
    using global::MongoDB.Bson;
    using global::MongoDB.Bson.Serialization.Attributes;
    using SharpCompress.Compressors.PPMd;
    using System.Security.Cryptography;
    using Web.Application.Domain.Entities;
    using Web.Application.Domain.Enums;
    using Web.Application.Domain.ValueObjects;

    public class UserSchema
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreateAt { get; set; }
        public EProfile Profile { get; set; }
        public PersonSchema Person { get; set; }

    }

    public static class UserSchemaExtension
    {
        public static Users ParseToDomain(this UserSchema document)
        {
            var user = new Users(
                document.Id,
                document.Username, 
                document.Email,
                document.Password,
                document.CreateAt,
                document.Profile
                );

            var person = new Person( 
                document.Person.ZipCode, document.Person.City,
                document.Person.Country, document.Person.State);

            user.AtributePerson(person);

            return user;
        }
    }

}
