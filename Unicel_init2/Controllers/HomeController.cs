using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Unicel_init2.Models;
using Unicel_init2.Repositories;

namespace Unicel_init2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFiltersRepository filtersRepository;

        public HomeController(ILogger<HomeController> logger, IFiltersRepository filtersRepository)
        {
            _logger = logger;
            this.filtersRepository = filtersRepository;
        }

        public async Task<IActionResult> Index()
        {
            var filters = await filtersRepository.GetAllAsync();

            return View(filters);
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