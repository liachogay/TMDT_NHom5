using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebMarket.Entities;
using WebMarket.Helpers;
using WebMarket.Models;


namespace WebMarket.Controllers
{
    public class CustomerController : Controller
    {

        private WebMarketContext _context;
        public CustomerController(WebMarketContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var user = @User.Claims.FirstOrDefault(c => c.Type == "Ma").Value;
            var role = @User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (user == null || role != "Customer")
            {
                var admin = _context.Admininfo.Find(Int32.Parse(user));
                ViewBag.user = admin;
                return View(admin);
            }
            var customer = _context.Customer.Find(Int32.Parse(user));
            ViewBag.user = customer;
            return View(customer);
        }
        public IActionResult Login()
        {
           
            ViewBag.Status = TempData["Message"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountVM acc)
        {
            var AccCus = _context.Account.SingleOrDefault(a => a.Username == acc.UserName && a.Password == acc.PassWord);
            if (AccCus == null)
            {
                ViewBag.Error = "Account not exsit";
                return View();
            }
            var customer = _context.Customer.SingleOrDefault(c => c.Id == AccCus.Id);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,customer.Name),
                new Claim(ClaimTypes.Email, customer.Email),
                new Claim("Ma", customer.Id.ToString()),
                new Claim(ClaimTypes.Role, "Customer"),
            };
            var userIdentity = new ClaimsIdentity(claims, "login");
            // create principal
            var principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal);
           
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Logout()
        {
            ExtensionHelper.isLogin = false;
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Status = TempData["Message"];
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM res)
        {
            var idAcc = _context.Account.SingleOrDefault(a => a.Username == res.account.UserName);
            if (idAcc == null)
            {
                var acc = new Account()
                {
                    Username = res.account.UserName,
                    Password = res.account.PassWord,
                    Type = 0,
                };
                _context.Account.Add(acc);
                _context.SaveChanges();
                int lastRow = _context.Account.OrderByDescending(a => a.Id).Select(a => a.Id).First();
                var cus = new Customer()
                {
                    Id = lastRow,
                    Name = res.customer.Name,
                    Address = res.customer.Address,
                    Phone = res.customer.Phone,
                    DateOfBirth = res.customer.Date,
                    Email = res.account.UserName,
                    Image = "~/images/img-customer/default.png",
                    Status = 0,
                };
                _context.Customer.Add(cus);
                _context.SaveChanges();
                return View("Login");
            }
            else
            {
                ViewBag.error = "* Email đã tồn tại";
                return View();
            }
        }
        public IActionResult LoginGmail(string type)
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponseRegister") };
            if (type == "login")
            {
                properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponseLogin") };
            }

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("google-response")]
        public async Task<IActionResult> GoogleResponseRegister()
        {

            var info = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var acc = new Account
            {
                Username = info.Principal.FindFirst(ClaimTypes.Email).Value,
                Type = 1,
            };
            var account = _context.Account.Where(a => a.Username == acc.Username).SingleOrDefault();
            if (account != null)
            {
                TempData["Message"] = "* Email đã tồn tại";
                return RedirectToAction("Register");
            }
            _context.Account.Add(acc);
            _context.SaveChanges();
            int lastRow = _context.Account.OrderByDescending(a => a.Id).Select(a => a.Id).First();
            var userInfo = new Customer()
            {
                Id = lastRow,
                Name = info.Principal.FindFirst(ClaimTypes.Name).Value,
                Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                Image = info.Principal.FindFirstValue("picture"),
                Status = 0,
            };
            _context.Customer.Add(userInfo);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> GoogleResponseLogin()
        {
            var info = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var acc = new Account
            {
                Username = info.Principal.FindFirst(ClaimTypes.Email).Value,
                Type = 1,
            };
            var account = _context.Account.Where(a => a.Username == acc.Username).SingleOrDefault();
            if (account == null)
            {
                TempData["Message"] = "* Email khong tồn tại";
                return RedirectToAction("Login");
            }
            var customer = _context.Customer.SingleOrDefault(c => c.Id == account.Id);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,customer.Name),
                new Claim(ClaimTypes.Email, customer.Email),
                new Claim("Ma", customer.Id.ToString()),
                new Claim(ClaimTypes.Role, "Customer"),
            };
            var userIdentity = new ClaimsIdentity(claims, "login");
            // create principal
            var principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            _context.Customer.Update(customer);
            _context.SaveChanges();
            ViewBag.user = customer;
            return View("Index");
        }

        public IActionResult BillCheck()
        {
            return PartialView("_Billcheck");
        }

    }
}