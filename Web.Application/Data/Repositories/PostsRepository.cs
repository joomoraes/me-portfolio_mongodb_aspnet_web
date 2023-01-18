
namespace Web.Application.Data.Repositories
{
    using global::MongoDB.Driver;
    using Microsoft.AspNetCore.Mvc;
    using Web.Application.Data.Schemas;
    using Web.Application.Domain.Entities;
    using Web.Application.Domain.ValueObjects;

    public class PostsRepository
    {
        IMongoCollection<PostSchema> _post;

        public PostsRepository(MongoDB mongoDB)
        {
            _post = mongoDB.DB.GetCollection<PostSchema>("post");
        }

        public void Insert(string userId, Post post)
        {
            var document = new PostSchema
            {
                Id = post.Id,
                Image = post.Image,
                Text = post.Text,
                Title = post.Title,
                UserId = post.UserId,
                LinkImage = post.LinkImage,
                CreateAt = DateTime.Now
            };

            _post.InsertOne(document);
        }

        public async Task<Dictionary<Post, double>> GetTop3_LookUp()
        {
            var ret = new Dictionary<Post, double>();

            var top3 = _post.Aggregate()
                .Group(_ => _.UserId, g => new
                {
                    UserId = g.Key,
                    AverageRelevances = g.Average(a => a.Relevance)
                })
                .SortByDescending(_ => _.AverageRelevances)
                .Limit(3)
                .Lookup<UserSchema, PostUserSchema>("user", "UserId", "Id", "User")
                .Lookup<PostSchema, PostUserSchema>("post", "Id", "UserId", "Post");

            await top3.ForEachAsync(_ =>
            {
                if (_.Users.Any())
                    return;

                var userPosts = new Post(
                    _.Post[0].Title, 
                    _.Post[0].Text, 
                    _.Post[0].Image,
                    _.Post[0].UserId,
                    _.Post[0].Relevance,
                    _.Post[0].LinkImage,
                    _.Post[0].CreateAt);

                ret.Add(userPosts, _.AverageRelevance);
            });

            return ret;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            var posts = new List<Post>();

            await _post.AsQueryable().ForEachAsync(d =>
            {
                var r = new Post(
                    d.Id,
                    d.Text,
                    d.Title,
                    d.UserId,
                    d.LinkImage,
                    d.CreateAt
                    );

                posts.Add(r);
            });

            return posts;
        }

        public async Task<Post> GetById(string id)
        {
            var document = _post.AsQueryable().FirstOrDefault(_ => _.Id == id);

            if (document == null)
                return null;

            return document.ParseToDomain();
        }

        public (long, long) Remove(string id)
        {
            var resultPost = _post.DeleteOne(_ => _.Id == id);

            return (resultPost.DeletedCount, resultPost.DeletedCount);
        }
    }
}
