using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMarket.Entities;

namespace WebMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductDetailController : Controller
    {
        private WebMarketContext _context;
        public ProductDetailController(WebMarketContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {
            ViewBag.product = _context.Product.SingleOrDefault(p=>p.Id==id);
            var product_detail = _context.Productdetail.Include(a => a.IdAdminNavigation).OrderByDescending(od => od.Id).Where(p => p.IdProduct == id).ToList();
            return View(product_detail);
        }
        [HttpPost]
        public IActionResult Create(Productdetail detail)
        {
            var user = @User.Claims.FirstOrDefault(c => c.Type == "Ma").Value;
            var newdetail = new Productdetail()
            {
                IdAdmin = Int32.Parse(user),
                IdProduct = detail.IdProduct,
                Quantity = detail.Quantity,
                EntryDate = detail.EntryDate,
                Mfg = detail.Mfg,
                Exp = detail.Exp
            };
            var product = _context.Product.Find(detail.IdProduct);
            product.QuantityStock += detail.Quantity;
            _context.Product.Update(product);
            _context.Add(newdetail);
            _context.SaveChanges();
            return RedirectToAction("Index",new { id= detail.IdProduct});
        }
        [HttpPost]
        public IActionResult FilterByDate(int id, DateTime dateFrom, DateTime dateTo)
        {
            ViewBag.product = _context.Product.SingleOrDefault(p => p.Id == id);
            string datestart = dateFrom.ToString("yyyy'-'MM'-'dd");
            string dateend = dateTo.ToString("yyyy'-'MM'-'dd");

            var product_detail = _context.Productdetail.Include(a => a.IdAdminNavigation).Include(a => a.IdProductNavigation).OrderByDescending(od => od.Id).Where(p => p.EntryDate >= dateFrom && p.EntryDate <= dateTo && p.IdProduct == id).ToList();
            
            ViewBag.date1 = dateFrom.ToString();
            ViewBag.date2 = dateTo.ToString();
            return View("Index", product_detail);

        }
    }
}
