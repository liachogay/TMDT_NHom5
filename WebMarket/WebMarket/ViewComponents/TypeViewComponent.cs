using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMarket.Entities;

namespace WebMarket.ViewComponents
{
    public class TypeViewComponent : ViewComponent
    {
        private WebMarketContext _context;
        public TypeViewComponent(WebMarketContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(string name)
        {
            var types =  from t in _context.Type
                        join c in _context.Category
                        on t.IdCategory equals c.Id
                        where c.Name == name
                        select t;
            ViewBag.namecate = name;
            return View(types);
        }
    }
}
