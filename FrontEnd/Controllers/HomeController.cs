using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            List<User>? users = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7044/");
                var usersResponse = await httpClient.GetAsync("GetUsers");
                if (usersResponse.IsSuccessStatusCode)
                {
                    var usersRaw = await usersResponse.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<User>>(usersRaw);
                }
            }

            return View(users);
        }

        public async Task<IActionResult> IndexPostsAsync()
        {
            List<Post>? posts = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7153/");
                var postsResponse = await httpClient.GetAsync("GetPosts");
                if (postsResponse.IsSuccessStatusCode)
                {
                    var postsRaw = await postsResponse.Content.ReadAsStringAsync();
                    posts = JsonConvert.DeserializeObject<List<Post>>(postsRaw);
                }
            }

            return View(posts);
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
    }
}