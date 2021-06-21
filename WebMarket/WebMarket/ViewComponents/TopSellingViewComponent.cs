using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMarket.Entities;
using WebMarket.Models;
using WebMarket.Secure;


namespace WebMarket.ViewComponents

{
    public class TopSellingViewComponent : ViewComponent
    {
        private WebMarketContext _context;
        private readonly IDataProtector protector;
        public TopSellingViewComponent(WebMarketContext context, IDataProtectionProvider dataProtectionProvider,
                              DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _context = context;
            this.protector = dataProtectionProvider.CreateProtector(
               dataProtectionPurposeStrings.ProductIdRouteValue);
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var sellitems = _context.Product
                .Include(p => p.IdTypeNavigation)
                .Where(p => p.Discount > 0)
                .Select(product => new ProductVM
                {
                    Id = product.Id,
                    EncryptedId = protector.Protect(product.Id.ToString()),
                    type1 = product.IdTypeNavigation.Name,
                    category = product.IdTypeNavigation.IdCategoryNavigation.Name,
                    Image = product.Image,
                    Name = product.Name,
                    Price = product.Price,
                    Discount = product.Discount,
                    NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
                })
                .ToList();

            var offeritems = _context.Product
                .Where(p => p.Discount > 0 && p.QuantitySold > 0)
                .Include(p => p.IdTypeNavigation)
                .Where(p => p.Discount > 0)
                .Select(product => new ProductVM
                {
                    Id = product.Id,
                    EncryptedId = protector.Protect(product.Id.ToString()),
                    type1 = product.IdTypeNavigation.Name,
                    category = product.IdTypeNavigation.IdCategoryNavigation.Name,
                    Image = product.Image,
                    Name = product.Name,
                    Price = product.Price,
                    Discount = product.Discount,
                    NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
                })
                .ToList();

            ViewBag.offeritems = offeritems;
            return View(sellitems);
        }
    }
}
