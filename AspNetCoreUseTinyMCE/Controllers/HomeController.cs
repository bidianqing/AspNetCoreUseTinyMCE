using AspNetCoreUseTinyMCE.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace AspNetCoreUseTinyMCE.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public HomeController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return Json(new { location = $"/uploads/{file.FileName}" });
        }
    }
}
