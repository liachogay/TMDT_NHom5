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
    public class CustomerManagerController : Controller
    {
        private WebMarketContext _context;
        public CustomerManagerController(WebMarketContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cusinfo = _context.Customer.Include(c=> c.IdNavigation).ToList();
            return View(cusinfo);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer customer, Account account)
        {
            var acc = new Account()
            {
                Username = account.Username,
                Password = account.Password,
                Type = account.Type
            };
            var cusinfo = new Customer()
            {
                Name = customer.Name,
                Address = customer.Address,
                Phone = customer.Phone,
                DateOfBirth = customer.DateOfBirth,
                Image = customer.Image,
                Email = customer.Email,
                Status = customer.Status
            };
            _context.Customer.Add(cusinfo);
            _context.SaveChanges();
            _context.Account.Add(acc);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var acc = _context.Account.SingleOrDefault(c => c.Id == id);
            var cus = _context.Customer.SingleOrDefault(c => c.Id == id);

            return View(cus);
        }
        [HttpPost]
        public IActionResult Edit(Customer customer, Account account)
        {

            _context.Update(customer);
            _context.Update(account);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
