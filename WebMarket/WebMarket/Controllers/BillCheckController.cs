using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMarket.Entities;

namespace WebMarket.Controllers
{
    public class BillCheckController : Controller
    {
        private readonly WebMarketContext _context;

        public BillCheckController(WebMarketContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int user = Int32.Parse(@User.Claims.FirstOrDefault(c => c.Type == "Ma").Value);
            ViewBag.order = _context.Order.Where(od => od.IdCustomer == user && od.Status == 0).ToList();
            var orderDetail = _context.Orderdetail.Include(x => x.IdOrderNavigation).Include(p => p.IdProductNavigation).ToList();
            return View(orderDetail);
        }
        [HttpPost]
        public IActionResult BillFilter(int status)
        {
            int user = Int32.Parse(@User.Claims.FirstOrDefault(c => c.Type == "Ma").Value);
            ViewBag.order = _context.Order
               .Where(od => od.IdCustomer == user && od.Status == status)
               .ToList();
            if (status == 3)
            {
                ViewBag.order = _context.Order.Where(od => od.IdCustomer == user && od.Status == status).ToList();
            }      
            var orderDetail = _context.Orderdetail
                .Include(x => x.IdOrderNavigation)
                .Include(p => p.IdProductNavigation)
                .ToList();
            return PartialView("_billtable",orderDetail);
        }
        [HttpPost]
        public IActionResult BillDelete(int id)
        {
            var order = _context.Order.FirstOrDefault(c => c.Id == id);
            order.Status = 4;
            _context.Update(order);
            _context.SaveChanges();
            ViewBag.order = _context.Order
               .Where(od => od.Status == 0)
               .ToList();
            var orderDetail = _context.Orderdetail
                .Include(x => x.IdOrderNavigation)
                .Include(p => p.IdProductNavigation)
                .ToList();
            return PartialView("_billtable",orderDetail);
        }
    }
}
