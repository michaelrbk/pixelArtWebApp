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
            using (var fileStream = new FileStream(Path.Combine(dir + "/wwwroot/uploads", string.Format(@"{0}.png", DateTime.Now.Ticks)), FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }
            return RedirectToAction("Index");
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
