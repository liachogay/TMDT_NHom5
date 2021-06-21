using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMarket.Entities;
using WebMarket.Helpers;

namespace WebMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BannerManagerController : Controller
    {
        private WebMarketContext _context;
        public BannerManagerController(WebMarketContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var banner = _context.Background.ToList();
            return View(banner);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Background background, IFormFile file)
        {
            string image = Helpers.ExtensionHelper.UploadFile(file,"img-banner");
            if (image == "error")
            {

                return Content("the image is null");
            };
            var newbg = new Background()
            {
                Name = background.Name,
                Image = image,
                Description = background.Description
            };
            _context.Add(newbg);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var background = _context.Background.SingleOrDefault(c => c.Id == id);
            return View(background);
        }
        [HttpPost]
        public IActionResult Edit(Background background, IFormFile file)
        {
            string image =  Helpers.ExtensionHelper.UploadFile(file, "img-banner");
            if (image != "error")
            {
                background.Image = image;
            }
            _context.Update(background);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var background = _context.Background.SingleOrDefault(c => c.Id == id);
            _context.Background.Remove(background);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
