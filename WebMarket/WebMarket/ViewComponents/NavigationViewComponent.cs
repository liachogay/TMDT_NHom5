using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMarket.Entities;

namespace WebMarket.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly WebMarketContext _context;
        public NavigationViewComponent(WebMarketContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Category.ToListAsync();
            var types = await _context.Type.ToListAsync();
            ViewBag.categories = categories;
            return View(types);
        }
    }
}
