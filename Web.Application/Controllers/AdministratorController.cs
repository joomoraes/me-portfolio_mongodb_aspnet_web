
namespace Web.Application.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Web.Application.Data.Repositories;
    using Web.Application.Domain.Dtos.Posts;
    using Web.Application.Domain.Dtos.Users;
    using Web.Application.Domain.Entities;
    using Web.Application.Domain.Enums;
    using Web.Application.Domain.ValueObjects;

    public class AdministratorController : Controller
    {
        private readonly UsersRepository _usersRepository;
        private readonly PostsRepository _postRepository;
        public AdministratorController(UsersRepository usersRepository,
            PostsRepository postsRepository)
        {
            _usersRepository = usersRepository;
            _postRepository = postsRepository;

        }
        public IActionResult Index()
            => View();

        [HttpGet]
        public async Task<IActionResult> UserRegister()
            =>  PartialView("Users/UserRegister");

        [HttpPost]
        public async Task<IActionResult> UserRegister(CreateDtoUsers users)
        {
            var profile = EProfileHelper.ParseToInt(users.Profile);

            var user = new Users(
                "",
                users.Username,
                users.Email,
                users.Password,
                DateTime.Now,
                profile);

            var person = new Person(
                users.ZipCode,
                users.City,
                users.State,
                users.Country);

            user.AtributePerson(person);

            if(!user._Validate())
            {
                return BadRequest(new
                {
                   error = user.ValidationResult.Errors.Select(_ => _.ErrorMessage)
                });
            }

             _usersRepository.Insert(user);

            return Ok(new
            {
                data = "Successful! Insert new user"
            });

        }

        [HttpGet]
        public async Task<IActionResult> FindUsers()
        {
            var users = await _usersRepository.GetAll();

            var list = users.Select(_ => new UserDto
            {
                Id = _.Id,
                Username = _.Username,
                Email = _.Email,
                Profile = _.Profile,
                Country = _.Person.Country ?? "",
                State = _.Person.State ?? "",
                ZipCode = _.Person.ZipCode ?? ""
            });

            return PartialView("Users/FindUsers", list);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _usersRepository.GetById(id);

            if (user == null)
                return NotFound();

            (var resultUserRemoved, var resultJornalsRemoved) = _usersRepository.Remove(id);

            return Ok(new
            {
                data = $"totall of exclude{resultUserRemoved} users with {resultJornalsRemoved} interactive"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetToUpdate(string id)
        {
            var user = await _usersRepository.GetById(id);

            if (user == null)
                return NotFound();

            var model = new UpdateDtoUsers
            {
                Id = id,
                Password = user.Password,
                Country = user.Person.Country,
                Email = user.Email,
                Profile = user.Profile,
                State = user.Person.State,
                Username = user.Username,
                City = user.Person.City,
                ZipCode = user.Person.ZipCode
            };

            return PartialView("Users/UpdateUser", model);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateDtoUsers models)
        {
            var user = await _usersRepository.GetById(models.Id);

            if (user == null)
                return NotFound();


            user = new Users(models.Id,
                models.Username,
                models.Email,
                models.Password,
                DateTime.Now,
                models.Profile);
            var personN = new Person(
                models.ZipCode,
                models.City,
                models.State,
                models.Country);

            user.AtributePerson(personN);

            if(!user._Validate())
            {
                return BadRequest(new
                {
                    errors = user.ValidationResult.Errors.Select(_ => _.ErrorMessage)
                });
            }

            if(!_usersRepository.Update(user))
            {
                return BadRequest(new
                {
                    data = "None documents was update!"
                });
            }

            return Ok(new
            {
                data = "Users was update successful"
            });
        }

       
        [HttpGet]
        public async Task<IActionResult> PostRegister()
            =>  PartialView("Posts/PostRegister");

        [HttpGet]
        public async Task<IActionResult> FindPosts()
        {
            var posts = await _postRepository.GetAll();

            var list = posts.Select(_ => new PostDto
            {
                Id = _.Id,
                Text = _.Text,
                Title = _.Title,
                Name = _usersRepository.GetById(_.UserId ?? "638671fc2a61c98808e19051").Result.Username ?? "",
                CreateAt = _.CreateAt
            });

            return PartialView("Posts/FindPosts", list);
        }

        [HttpPost]
        public async Task<IActionResult> PostRegister(CreateDtoPosts posts)
        {
            var post = new Post(
                posts.Title,
                posts.Text,
                posts.Image.ToString(),
                1,
                DateTime.Now,
                posts.UserId
                );

            if(!post._Validate())
            {
                return BadRequest(new
                {
                    error = post.ValidationResult.Errors.Select(_ => _.ErrorMessage)
                });
            }

            _postRepository.Insert(post.UserId, post);

            return Ok(new
            {
                data = "Successful! Insert new user"
            });
        }

        [HttpGet]
        public async Task<IActionResult> PostDelete(string id)
        {
            var post = await _postRepository.GetById(id);

            if (post == null)
                return NotFound();

            (var resultPostRemoved, var resultRemoved) = _postRepository.Remove(id);

            return Ok(new
            {
                data = $"total of excluded {resultPostRemoved} user with {resultRemoved} interctive"
            });

        }
    }

}
