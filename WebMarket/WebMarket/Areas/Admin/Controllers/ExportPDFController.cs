using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa;
using Rotativa.AspNetCore;
using WebMarket.Areas.Admin.Models;
using WebMarket.Entities;

namespace WebMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExportPDFController : Controller
    {
        private WebMarketContext _context;
        public ExportPDFController(WebMarketContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PrintAllReport(int id)
        {
            var dorder = _context.Order.Include(o=>o.IdCustomerNavigation).Where(d => d.Id == id).SingleOrDefault();
            var ddetail = _context.Orderdetail.Include(o => o.IdProductNavigation).Where(d => d.IdOrder == id).ToList();
            Bill bill = new Bill()
            {
                order = dorder,
                orderdetail = ddetail,
            };
           
            return new ViewAsPdf("Index",bill);
        }
    }
}
