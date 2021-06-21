using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMarket.Entities;

namespace WebMarket.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class PriceUpdatedManagerController : Controller
    {
        private WebMarketContext _context;
        public PriceUpdatedManagerController(WebMarketContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            var queryables = _context.Priceupdate.FromSqlRaw("SELECT * FROM dbo.priceupdate FOR SYSTEM_TIME ALL").AsNoTracking().OrderByDescending(od => od.DateUpdate)
                .Include(p => p.IdProductNavigation)
                .Include(a => a.IdAdminNavigation).ToList();
            return View(queryables);
        }
        public IActionResult PriceUpdateByProduct(int id)
        {
            var queryables = _context.Priceupdate.FromSqlRaw("SELECT * FROM dbo.priceupdate FOR SYSTEM_TIME ALL")
                .AsNoTracking()
                .OrderByDescending(od => od.DateUpdate)
                .Include(p => p.IdProductNavigation)
                .Include(a => a.IdAdminNavigation)
                .Where(p => p.IdProduct == id)
                .ToList();
            ViewBag.id = id;
            return View("Index", queryables);
        }
        [HttpPost]
        public IActionResult PriceUpdateByProduct(int id, DateTime dateFrom , DateTime dateTo)
        {    
                string datestart = dateFrom.ToString("yyyy'-'MM'-'dd");
                string dateend = dateTo.ToString("yyyy'-'MM'-'dd");

                var queryables = _context.Priceupdate.FromSqlRaw("SELECT * FROM dbo.priceupdate FOR SYSTEM_TIME BETWEEN '"+datestart+"' AND '"+ dateend+ "'")
                    .AsNoTracking()
                    .OrderByDescending(od => od.DateUpdate)
                    .Include(p => p.IdProductNavigation)
                    .Include(a => a.IdAdminNavigation)
                    .Where(p => p.IdProduct == id)
                    .ToList();
                ViewBag.date1 = dateFrom.ToString();
                ViewBag.date2 = dateTo.ToString();
                return View("Index", queryables);

        }

    }
}
