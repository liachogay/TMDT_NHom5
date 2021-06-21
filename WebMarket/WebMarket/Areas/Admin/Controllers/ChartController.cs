using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMarket.Areas.Admin.Models;
using WebMarket.Entities;
using WebMarket.Models;

namespace WebMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChartController : Controller
    {
        
        private WebMarketContext _context;
        public ChartController(WebMarketContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.bill0 = _context.Order.Count(x => x.Status == 0);
            ViewBag.bill1 = _context.Order.Count(x => x.Status == 1);
            ViewBag.bill2 = _context.Order.Count(x => x.Status == 2);
            var category = _context.Category.ToList();
            ViewBag.IdCategory = new SelectList(category, "Id", "Name");
            return View();
        }
        [HttpGet]
        public List<TypeChart> TypeChart(int id=1)
        {
            List<TypeChart> lst = new List<TypeChart>();
            lst = (from t in _context.Type
                        join od in _context.Product on t.Id equals od.IdType
                        where t.IdCategory == id
                        group od by t.Name into chart
                        select new TypeChart()
                        {
                            Name = chart.Key,
                            count = chart.Count()
                        }).ToList();
            return lst;
        }
        public List<ProductSellChart> SellChart(string nameType)
        {

            var lst = (from p in _context.Product
                       join od in _context.Orderdetail on p.Id equals od.IdProduct
                       where p.IdTypeNavigation.Name == nameType
                       group od by p.Name into chart
                       select new ProductSellChart()
                       {
                           Name = chart.Key,
                           Sold = chart.Sum(p => p.Quantity)
                       }).ToList();
          
            return lst;
        }
    }
}
