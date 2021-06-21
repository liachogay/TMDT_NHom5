using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMarket.Entities;

namespace WebMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminManagerController : Controller
    {
        private WebMarketContext _context;
        public AdminManagerController(WebMarketContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Admininfo.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create( Admininfo admininfo)
        {
            var admin = new Admininfo()
            {
                Name = admininfo.Name,
                Username = admininfo.Username,
                Password = admininfo.Password,
                Address = admininfo.Address,
                Phone = admininfo.Phone,
                Type = admininfo.Type
            };
            _context.Admininfo.Add(admin);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var admin = _context.Admininfo.SingleOrDefault(c => c.Id == id);
            return View(admin);
        }
        [HttpPost]
        public IActionResult Edit(Admininfo admininfo)
        {
            _context.Update(admininfo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
