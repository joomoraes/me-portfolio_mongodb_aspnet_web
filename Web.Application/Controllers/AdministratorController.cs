
namespace Web.Application.Controllers
{

using Microsoft.AspNetCore.Mvc;
    using Web.Application.Controllers.Inputs;
    using Web.Application.Controllers.Outputs;
    using Web.Application.Data.Repositories;
    using Web.Application.Domain.Entities;
    using Web.Application.Domain.Enums;
    using Web.Application.Domain.ValueObjects;

    public class AdministratorController : Controller
    {
        private readonly UsersRepository _usersRepository;
        public AdministratorController(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> UserRegister()
            => PartialView();

        [HttpPost]
        public async Task<IActionResult> UserRegister(IncludeUsers users)
        {
            var profile = EProfileHelper.ParseToInt(users.Profile);

            var user = new Users(
                "",
                users.Username,
                users.Email,
                users.Password,
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
        public async Task<ActionResult> FindUsers()
        {
            var users = await _usersRepository.GetAll();

            var list = users.Select(_ => new UsersList
            {
                Id = _.Id,
                Username = _.Username,
                Email = _.Email,
                Profile = _.Profile,
                Country = _.Person.Country ?? "",
                State = _.Person.State ?? "",
                ZipCode = _.Person.ZipCode ?? ""
            });

            return PartialView(list);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = _usersRepository.GetById(id);

            if (user == null)
                return NotFound();

            (var resultUserRemoved, var resultJornalsRemoved) = _usersRepository.Remove(id);

            return Ok(new
            {
                data = $"totall of exclude{resultUserRemoved} users with {resultJornalsRemoved} interactive"
            });
        }

        [HttpGet]
        public async Task<ActionResult> GetToUpdate(string id)
        {
            var user = await _usersRepository.GetById(id);

            if (user == null)
                return NotFound();

            var model = new UserUpdate
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

            return PartialView("UpdateUser", model);

        }

        [HttpPost]
        public async Task<ActionResult> UpdateUser(UserUpdate models)
        {
            var user = await _usersRepository.GetById(models.Id);

            if (user == null)
                return NotFound();


            user = new Users(models.Id, models.Username, models.Email, models.Password, models.Profile);
            var personN = new Person(models.ZipCode, models.City, models.State, models.Country);

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

    }

}
