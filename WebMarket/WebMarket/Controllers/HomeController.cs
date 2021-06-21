using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebMarket.Models;
using WebMarket.Entities;


namespace WebMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WebMarketContext _context;

        public HomeController(ILogger<HomeController> logger, WebMarketContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var typess = (from type in _context.Type
                          from Category in _context.Category.Where(i => i.Id == type.IdCategory).Take(1)
                          select new ProductVM
                          {
                              type1 = type.Name,

                          }).ToList();
            ViewBag.type2 = typess;

            var backgrounds = _context.Background.ToList();
            ViewBag.background = backgrounds;
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

        public IActionResult Offers()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}
