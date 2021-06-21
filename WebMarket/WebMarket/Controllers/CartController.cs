using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMarket.Entities;
using WebMarket.Helpers;
using WebMarket.Models;
using CartItem = WebMarket.Models.CartItem;
namespace WebMarket.Controllers
{
    public class CartController : Controller
    {
        private readonly WebMarketContext _context;

        public CartController(WebMarketContext context)
        {
            _context = context;
        }

        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (data == null)
                {
                    data = new List<CartItem>();
                }
                return data;
            }
        }

        public IActionResult Index(CartItem cart)
        {
            double? dTongTien = Carts.Sum(p => p.TotalPrice);
            string sTongTien = $"{dTongTien:#,##0.00} đ";
            ViewBag.TongTien = sTongTien;
            var name = cart.Name;
            ViewBag.data = HttpContext.Session.GetString("name");
            return View(Carts);
        }
        [TempData]
        public string TotalQuantity { get; set; }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.Id == id);
            if (item == null)//chưa có
            {
                item = (from product in _context.Product.Where(p => p.Id == id)
                        select new CartItem
                        {
                            Id = product.Id,
                            Image = product.Image,
                            Name = product.Name,
                            Price = product.Price.Value,
                            Discount = product.Discount,
                            Quantity = quantity
                        }).SingleOrDefault();
                myCart.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }
            HttpContext.Session.Set("GioHang", myCart);
            quantity = Carts.Sum(c => c.Quantity);
            return Json(new { myCart, quantity });
        }
        [HttpPost]
        public IActionResult UpdateCart(int Id, int Quantity)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.Id == Id);
            int index = myCart.IndexOf(item);
            myCart[index].Quantity = Quantity;
            HttpContext.Session.Set("GioHang", myCart);
            double? totalprice = Carts.Sum(p => p.TotalPrice);
            int quantity = Carts.Sum(c => c.Quantity);
            return Json(new { TongTien = totalprice, SoLuong = quantity });
        }
        [HttpPost]
        public IActionResult DeleteItem(int Id)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.Id == Id);
            myCart.Remove(item);
            HttpContext.Session.Set("GioHang", myCart);
            double? totalprice = Carts.Sum(p => p.TotalPrice);
            int quantity = Carts.Sum(c => c.Quantity);
            return Json(new { TongTien = totalprice, SoLuong = quantity });
        }
    }
}