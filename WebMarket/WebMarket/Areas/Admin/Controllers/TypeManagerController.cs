using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMarket.Entities;
using Type = WebMarket.Entities.Type;

namespace WebMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TypeManagerController : Controller
    {
        private WebMarketContext _context;
        public TypeManagerController(WebMarketContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cate = _context.Category.ToList();
            
            Category s = new Category()
            {
                Id = 0,
                Name = "All Type",
          
            };
            cate.Insert(0, s);
            ViewBag.IdCategory = new SelectList(cate, "Id", "Name");
            var types = _context.Type
                .Include(c => c.IdCategoryNavigation).ToList();
           
            return View(types);
            
        }
        public IActionResult filterajax(int cate=0)
        {
            var types = _context.Type.ToList();
            if (cate != 0)
            {
                types = _context.Type
                   .Include(c => c.IdCategoryNavigation)
                   .Where(t => t.IdCategory == cate).ToList();
            }
            return PartialView("_filterajax", types);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var cate = _context.Category.ToList();
            ViewBag.IdCategory = new SelectList(cate, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Type type)
        {
            var newtype = new Type()
            {
                Name = type.Name,
                IdCategory = type.IdCategory
            };  
            _context.Type.Add(type);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var type = _context.Type.SingleOrDefault(t => t.Id == id);
            var cate = _context.Category.ToList();
            ViewBag.IdCategory = new SelectList(cate, "Id", "Name");
            return View(type);
        }
        [HttpPost]
        public IActionResult Edit(Type type)
        {
           
            _context.Update(type);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var products = _context.Product.Where(p=>p.IdType==id).ToList();
            var type = _context.Type.SingleOrDefault(c => c.Id == id);
            foreach (var idx in products)
            {
                var priceupdate = _context.Priceupdate.Where(p=>p.IdProduct == idx.Id).ToList();
                foreach( var item in priceupdate)
                {
                    _context.Priceupdate.Remove(item);
                }
            }
            _context.Product.RemoveRange(products);
            _context.Type.Remove(type);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
