
namespace Web.Application.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Reflection;
    using System.Security.Claims;
    using System.Security.Principal;
    using Web.Application.Data.Repositories;
    using Web.Application.Domain.Dtos;
    using Web.Application.Domain.Dtos.Posts;
    using Web.Application.Domain.Dtos.Users;
    using Web.Application.Domain.Entities;
    using Web.Application.Domain.Enums;
    using Web.Application.Domain.Security;
    using Web.Application.Domain.ValueObjects;

    public class AdministratorController : Controller
    {
        private readonly UsersRepository _usersRepository;
        private readonly PostsRepository _postRepository;
        public SigningConfigurations _signingConfigurations;

        public AdministratorController(UsersRepository usersRepository,
            PostsRepository postsRepository,
                            SigningConfigurations signingConfigurations)
        {
            _usersRepository = usersRepository;
            _postRepository = postsRepository;
            _signingConfigurations = signingConfigurations;
        }

        public IActionResult Index()
            => View();
        

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            var login = new LoginDto("jp10y@hotmail.com", "123456");


            if (login == null)
                return BadRequest();

            try
            {
                var result = await LoginService(login.Email, login.Password);
                if(result != null)
                {
                    return Ok(result);
                } else
                {
                    return NotFound();
                }
            }
            catch (ArgumentException ex)
            {

                throw;
            }
        }
        

        [HttpGet]
        [Authorize("Bearer")]
        public async Task<IActionResult> UserRegister()
            =>  PartialView("Users/UserRegister");

        [HttpPost]
        [Authorize("Bearer")]
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
        [Authorize("Bearer")]
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
        [Authorize("Bearer")]
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
        [Authorize("Bearer")]
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
        [Authorize("Bearer")]
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
        [Authorize("Bearer")]
        public async Task<IActionResult> PostRegister()
            =>  PartialView("Posts/PostRegister");

        [HttpGet]
        [Authorize("Bearer")]
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
        [Authorize("Bearer")]
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
        [Authorize("Bearer")]
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

        public async Task<object> LoginService(string Email, string Password)
        {
            var login = new LoginDto(Email, Password);

            if (login != null
                && !string.IsNullOrWhiteSpace(login.Email)
                && !string.IsNullOrWhiteSpace(login.Password))
            {
                var baseUser = await _usersRepository.FindByLogin(login.Email, login.Password);

                if (baseUser == null)
                {
                    return new
                    {
                        authenticated = false,
                        message = "Fail authenmtication"
                    };
                }
                else
                {

                    ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(login.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, login.Email)
                        }
                    );

                    var pegar = Environment.GetEnvironmentVariable("Seconds");

                    DateTime createDate = DateTime.UtcNow;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(Convert.ToInt32(Environment.GetEnvironmentVariable("Seconds")));

                    var handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, createDate, expirationDate, handler);

                    var user = await _usersRepository.GetById(baseUser.Id);
                    var person = _usersRepository.GetById(baseUser.Id).Result.Person;

                    user = new Users(baseUser.Id,
                        baseUser.Username,
                        baseUser.Email,
                        baseUser.Password,
                        DateTime.Now,
                        baseUser.Profile,
                        token);
                    var personN = new Person(
                           person.ZipCode,
                           person.City,
                           person.State,
                           person.Country);

                    user.AtributePerson(personN);

                    if (!_usersRepository.Update(user))
                    {
                        return BadRequest(new
                        {
                            data = "None documents was update!"
                        });
                    }
                    return SuccessObject(createDate, expirationDate, token, baseUser);
                }
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
        }

        private string CreateToken(ClaimsIdentity identity,
            DateTime createDate, 
            DateTime expirationDate,
            JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Environment.GetEnvironmentVariable("Issuer"),
                Audience = Environment.GetEnvironmentVariable("Audience"),
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, Users user)
        {
            return new
            {
                authenticated = true,
                create = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                userName = user.Email,
                name = user.Username,
                message = "Users log-in"
            };
        }

    }

}
