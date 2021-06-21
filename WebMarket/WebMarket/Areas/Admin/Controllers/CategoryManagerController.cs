using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMarket.Entities;
using WebMarket.Helpers;

namespace WebMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryManagerController : Controller
    {
        private WebMarketContext _context;
        public CategoryManagerController(WebMarketContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Category.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category, IFormFile file)
        {
            string image = Helpers.ExtensionHelper.UploadFile(file);
            if(image == "error")
            {

                return Content("the image is null");
            }
            var newcate = new Category()
            {
                Name = category.Name,
                Image = image
            };
            _context.Category.Add(newcate);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _context.Category.SingleOrDefault(c => c.Id == id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category,IFormFile file)
        {
            string image = Helpers.ExtensionHelper.UploadFile(file);
            if (image != "error")
            {
                category.Image = image;
            }
            _context.Update(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var products = (from product in _context.Product
                           join type in _context.Type
                           on product.IdType equals type.Id
                           join cate in _context.Category
                           on type.IdCategory equals cate.Id
                           where cate.Id == id
                           select product).ToList();
            var types = _context.Type.Where(t => t.IdCategory == id).ToList();
            var category = _context.Category.SingleOrDefault(c => c.Id == id);
            foreach (var idx in products)
            {
                var priceupdate = _context.Priceupdate.Where(p => p.IdProduct == idx.Id).ToList();
                foreach (var item in priceupdate)
                {
                    _context.Priceupdate.Remove(item);
                }
            }

            _context.Product.RemoveRange(products);
            _context.Type.RemoveRange(types);
            _context.Category.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
