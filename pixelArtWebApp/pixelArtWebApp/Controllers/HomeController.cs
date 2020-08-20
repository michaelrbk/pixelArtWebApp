using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using pixelArtWebApp.Models;
using System;
using System.Diagnostics;
using System.IO;

namespace pixelArtWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHostEnvironment _env;
        public IActionResult SingleFile(IFormFile file)
        {
            var dir = _env.ContentRootPath;
            var fileName = string.Format(@"{0}.png", DateTime.Now.Ticks);
            var filePath = Path.Combine(dir, "wwwroot", "uploads", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }
            ViewData["FileLocation"] = $"/uploads/{fileName}";
            return View("../Home/Index");
        }
        public HomeController(IHostEnvironment env, ILogger<HomeController> logger)
        {
            _env = env;
            _logger = logger;
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
    }
}
