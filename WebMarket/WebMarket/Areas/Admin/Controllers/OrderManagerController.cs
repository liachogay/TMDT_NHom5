using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMarket.Entities;
using WebMarket.Helpers;
using WebMarket.Models;

namespace WebMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderManagerController : Controller
    {
        private WebMarketContext _context;
        public OrderManagerController(WebMarketContext context)
        {
            _context = context;
        }
        
      
        public IActionResult Index(int? status)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            var order = _context.Order.Where(p=>p.Status ==status).ToList();
            return View(order);
        }

        [HttpPost]
        public ActionResult Detail(int id)
        {
            var detail = _context.Orderdetail.Include(ord => ord.IdOrderNavigation).Include(pro => pro.IdProductNavigation).Where(p => p.IdOrder == id).ToList();
            ViewBag.order = _context.Order.Include(ord => ord.IdCustomerNavigation).Where(p => p.Id == id).SingleOrDefault();
            return PartialView("_OrderPatial", detail);

        }

        [HttpPost]
        public IActionResult StatusChange(int Id_order, int status_order)
        {

            
            var order = _context.Order.SingleOrDefault(od => od.Id == Id_order);
            var user = @User.Claims.FirstOrDefault(c => c.Type == "Ma").Value;

            var orderupdate = new Orderupdate()
            {
                IdOrder = Id_order,
                IdAdmin = Int32.Parse(user),
                OldStatus = order.Status,
                NewStatus = status_order,
                DateUpdate = default,
                DateEnd = default,
            };

            order.IdAdmin = Int32.Parse(user);
            order.Status = status_order;
            _context.Orderupdate.Update(orderupdate);
            _context.Update(order);
            _context.SaveChanges();
            
            return RedirectToAction("Index", new { status = status_order - 1 });

        }
        [HttpPost]
        public ActionResult History(int id)
        {
            var queryables = _context.Orderupdate.FromSqlRaw("SELECT * FROM dbo.Orderupdate FOR SYSTEM_TIME ALL")
                .AsNoTracking()
                .OrderByDescending(od => od.DateUpdate)
                .Include(p => p.IdOrderNavigation)
                .Include(a => a.IdAdminNavigation)
                .Where(p => p.IdOrder == id)
                .ToList();
            ViewBag.id = id;
            return PartialView("_HistoryPatial", queryables);
        }
    }
}
