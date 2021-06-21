using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMarket.Entities;
using WebMarket.Helpers;
using WebMarket.Models;
namespace WebMarket.Controllers
{
    public class Thanhtoan : Controller
    {

        private WebMarketContext _context;
        public Thanhtoan(WebMarketContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if(!@User.Claims.Any(c => c.Type == "Ma"))
            {
                return RedirectToAction("Login", "Customer");
            }
            int customer = Int32.Parse(@User.Claims.FirstOrDefault(c => c.Type == "Ma").Value);
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            if (cart != null)
            {
                double? TongTien = cart.Sum(p => p.TotalPrice);
                string sTongTien = $"{TongTien:#,##0.00} đ";
                ViewBag.TongTien = sTongTien;
                int Quanity = cart.Sum(c => c.Quantity);
                ViewBag.Quanity = Quanity;
            }
            var customer_info = _context.Customer.Find(customer);
            ViewBag.cart = cart;
            return View(customer_info);
            
        }
        [HttpPost]
        public IActionResult Pay(Customer customer)
        {
            int id =Int32.Parse(@User.Claims.FirstOrDefault(c => c.Type == "Ma").Value);
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            double? TongTien = cart.Sum(p => p.TotalPrice);
        
            
            Order ord = new Order();
            ord.Name = customer.Name;
            ord.Address = customer.Address;
            ord.OrderDate = DateTime.Now;
            ord.IdAdmin = 1;
            ord.IdCustomer = id;
            ord.Phone = customer.Phone;
            ord.TotalPrice = TongTien;
            _context.Order.Add(ord);
            _context.SaveChanges();

            int lastOrder = _context.Order.OrderByDescending(a => a.Id).Select(a => a.Id).First(); 
            
            foreach (var item in cart)
            {  
                var orderdetail = new Orderdetail
                {
                    IdOrder = lastOrder,
                    IdProduct = item.Id,
                    Quantity = item.Quantity,
                    Discount = item.Discount,
                    Newprice = item.NewPrice
                };
                _context.Orderdetail.Add(orderdetail);
                var product = _context.Product.Find(item.Id);
                product.QuantityStock -= item.Quantity;
                product.QuantitySold += item.Quantity;
                _context.Product.Update(product);

            }
            _context.SaveChanges();

            var orderupdate = new Orderupdate()
            {
                IdOrder = lastOrder,
                NewStatus = ord.Status,
                DateUpdate = default,
                DateEnd = default,
            };
            _context.Orderupdate.Add(orderupdate);
            _context.SaveChanges();
            var session = HttpContext.Session;
            session.Remove("GioHang");
                 
        
            return RedirectToAction("Index", "Cart");
        }
    }
}
