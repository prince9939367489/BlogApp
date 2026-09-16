using System.Diagnostics;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string? q, string? category)
        {
            ViewData["Query"] = (q ?? "").Trim();
            ViewData["Category"] = category ?? "";
            return View(PostCatalog.All);
        }

        [HttpGet("/stories/{slug}")]
        public IActionResult Article(string slug)
        {
            var post = PostCatalog.All.FirstOrDefault(p => p.Slug == slug);
            return post is null ? NotFound() : View(post);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
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
