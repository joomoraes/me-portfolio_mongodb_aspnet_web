namespace Web.Application.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Net.Http.Headers;
    using Web.Application.Data.Repositories;
    using Web.Application.Domain.Dtos.Posts;
    using Web.Application.Domain.Dtos.Views;
    using Web.Application.Domain.Entities;
    using Web.Application.Domain.ValueObjects;
    using Web.Application.Models;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PostsRepository _postRepository;
        private readonly ViewsRepository _viewRepository;

        public HomeController(ILogger<HomeController> logger,
            PostsRepository postsRepository,
            ViewsRepository viewsRepository)
        {
            _logger = logger;
            _postRepository = postsRepository;
            _viewRepository = viewsRepository;
        }

        public IActionResult Index()
        { 
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> NewVisitor(CreateDtoViews views)
        {


            var ip = "blocked";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ipify.org/?format=jsonp&callback=ip");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {  //GET
                   ip = response.Content.ReadAsStringAsync().Result;
                }
            }


                if (_viewRepository.GetLast(ip).Result.UserIp != null)
            {
                return BadRequest(new
                {
                    data = "Try inject repeat detect"
                });
            } else { 

                var view = new Views(
                    views.Id,
                    views.Latitude,
                    views.Longitude,
                    ip,
                    views.createDate
                    );

                if (!view._Validate())
                {
                    return BadRequest(new
                    {
                        error = view.ValidationResult.Errors.Select(_ => _.ErrorMessage)
                    });
                }

                _viewRepository.Insert(view);

                return Ok(new
                {
                    data = "Successful! Insert data visitor"
                });
            }
            
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = _postRepository.GetAll();
            var document = new List<PostDto>();
            foreach (var item in posts.Result.ToList() ?? new List<Post>())
            {
                document.Add(new PostDto()
                {
                    CreateAt = item.CreateAt,
                    Image = item.Image,
                    LinkImage = item.LinkImage,
                    Text = item.Text,
                    Title = item.Title
                });
            }

            return PartialView(document);
        }

    }
}