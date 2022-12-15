namespace Web.Application.Data.Repositories
{
    using global::MongoDB.Driver;
    using Web.Application.Data.Schemas;
    using Web.Application.Domain.Dtos;
    using Web.Application.Domain.Entities;
    using Web.Application.Domain.ValueObjects;
    using ZstdSharp.Unsafe;

    public class UsersRepository 
    {
        IMongoCollection<UserSchema> _users;

        public UsersRepository(MongoDB mongoDB)
        {
            _users = mongoDB.DB.GetCollection<UserSchema>("users");
        }

        public async Task<Users> FindByLogin(string email, string password)
        {
            var document = _users.AsQueryable().FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));

            if (document == null)
                return null;

            return document.ParseToDomain();
        }

        public void Insert(Users users)
        {
            var document = new UserSchema
            {
                Username = users.Username,
                Email = users.Email,
                Password = users.Password,
                Profile = users.Profile,
                CreateAt = users.CreateAt,
                Person = new PersonSchema
                {
                    Biograpphy = users.Person.Biography,
                    City = users.Person.City,
                    Country = users.Person.Country,
                    State = users.Person.State,
                    ZipCode = users.Person.ZipCode
                }
            };

            _users.InsertOne(document);
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            var users = new List<Users>();

            await _users.AsQueryable().ForEachAsync(d =>
            {
                var r = new Users(
                    d.Id,
                    d.Username,
                    d.Email,
                    d.Password,
                    d.CreateAt,
                    d.Profile);
                var e = new Person(d.Person.City,
                    d.Person.Country,
                    d.Person.State,
                    d.Person.ZipCode);
                r.AtributePerson(e);

                users.Add(r);
            });

            return users;
        }

        public (long, long) Remove(string userId)
        {
            var resultUser = _users.DeleteOne(_ => _.Id == userId);

            return (resultUser.DeletedCount, resultUser.DeletedCount);
        }

        public async Task<Users> GetById(string id)
        {
            var document = _users.AsQueryable().FirstOrDefault(_ => _.Id == id);

            if (document == null)
                return null;

            return document.ParseToDomain();
        }

        public bool Update(Users users)
        {
            var document = new UserSchema
            {
                Id = users.Id,
                Email = users.Email,
                Password = users.Password,
                Profile = users.Profile,
                Username = users.Username,
                Person = new PersonSchema
                {
                    Biograpphy = users.Person.Biography,
                    City = users.Person.City,
                    Country = users.Person.Country,
                    Medals = users.Person.Medals,
                    State = users.Person.State,
                    TotalSponser = users.Person.TotalSponser,
                    ZipCode = users.Person.ZipCode
                }
            };

            var result = _users.ReplaceOne(_ => _.Id == document.Id, document);

            return result.ModifiedCount > 0;
        }
    }
}
