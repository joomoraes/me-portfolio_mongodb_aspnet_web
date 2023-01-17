namespace Web.Application.Data
{
    using global::MongoDB.Bson;
    using global::MongoDB.Bson.Serialization;
    using global::MongoDB.Bson.Serialization.Serializers;
    using global::MongoDB.Driver;
    using Web.Application.Data.Schemas;
    using Web.Application.Domain.Enums;

    public class MongoDB
    {
        public IMongoDatabase DB { get; }

        public MongoDB(IConfiguration configuration)
        {
            try
            {
                var client = new MongoClient(configuration.GetConnectionString("MongoDB"));
                DB = client.GetDatabase(configuration["NameDB"]);
                MapClasses();
            }
            catch (Exception ex)
            {
                throw new MongoException("We can't connection with MongoDB", ex);
            }
        }

        private void MapClasses()
        {
            if(!BsonClassMap.IsClassMapRegistered(typeof(UserSchema)))
            {
                BsonClassMap.RegisterClassMap<UserSchema>(i =>
                {
                    i.AutoMap();
                    i.MapMember(c => c.Id);
                    i.MapMember(c => c.Profile).SetSerializer(new EnumSerializer<EProfile>(BsonType.Int32));
                    i.SetIgnoreExtraElements(true);
                });
            }       
            
            if(!BsonClassMap.IsClassMapRegistered(typeof(LoginSchema)))
            {
                BsonClassMap.RegisterClassMap<LoginSchema>(i =>
                {
                    i.AutoMap();
                    i.MapMember(c => c.Id);
                    i.MapMember(c => c.status).SetSerializer(new EnumSerializer<EStatusLogin>(BsonType.Int32));
                    i.SetIgnoreExtraElements(true);
                });
            }

            if(!BsonClassMap.IsClassMapRegistered(typeof(ViewsSchema)))
            {
                BsonClassMap.RegisterClassMap<ViewsSchema>(i =>
                {
                    i.AutoMap();
                    i.MapMember(c => c.Id);
                    i.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}
