using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebMarket.Entities;
using WebMarket.Models;
using WebMarket.Secure;

namespace WebMarket.Controllers
{
    public class CategoryController : Controller
    {
        private readonly WebMarketContext _context;
        private readonly IDataProtector protector;

        public CategoryController(WebMarketContext context, IDataProtectionProvider dataProtectionProvider,
                              DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _context = context;
            this.protector = dataProtectionProvider.CreateProtector(
               dataProtectionPurposeStrings.ProductIdRouteValue);
        }

       
        private int numpage = 15;

       
        [HttpGet("Category/{name}")]
        [HttpGet("Category/{name}/data={data}")]
        [HttpGet("Category/{name}/minPrice={minPrice}&&maxPrice={maxPrice}")]
        public   IActionResult Index(string name, int data, string searchString, float minPrice, float maxPrice, int page = 1 )

        {
            if (minPrice != 0 && maxPrice != 0)
            {
                if (data == 1)
                {
                    var pricePro = (from product in _context.Product.Where(x => x.Price >= minPrice && x.Price <= maxPrice)
                                    from cate in _context.Category.Where(c => c.Name == name)
                                    join type in _context.Type
                                    on product.IdType equals type.Id
                                    where type.IdCategory == cate.Id
                                    orderby product.Price
                                    select new ProductVM
                                    {
                                        Id = product.Id,
                                        EncryptedId = protector.Protect(product.Id.ToString()),
                                        type1 = type.Name,
                                        Image = product.Image,
                                        Name = product.Name,
                                        Price = product.Price,
                                        Discount = product.Discount,
                                        Description = product.Description,
                                        NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
                                    }).ToList().Skip((page - 1) * numpage).Take(numpage);
                    int countsss;
                    if (pricePro.Count() == 0)
                    {
                        countsss = 0;
                    }
                    else
                    {
                        countsss = (
                       from product in _context.Product.Where(x => x.Price >= minPrice && x.Price <= maxPrice && x.Discount > 0)
                       from cate in _context.Category.Where(c => c.Name == name)
                       join type in _context.Type
                       on cate.Id equals type.IdCategory
                       where product.IdType == type.Id
                       select product).Count();
                    }
                    ViewBag.name = name;
                    ViewBag.total = (Int32)(Math.Ceiling((float)countsss / numpage));
                    ViewBag.currentpage = page;
                    ViewBag.min = minPrice;
                    ViewBag.max = maxPrice;
                    ViewBag.da = data;
                    return View(pricePro);

                }
            }
            if (minPrice != 0 && maxPrice != 0)
            {
                if (data == 2)
                {
                    var pricePro = (from product in _context.Product.Where(x => x.Price >= minPrice && x.Price <= maxPrice ).OrderByDescending(l=>l.Price)
                                    from cate in _context.Category.Where(c => c.Name == name)
                                    join type in _context.Type
                                    on product.IdType equals type.Id
                                    where type.IdCategory == cate.Id
                                  
                                    select new ProductVM
                                    {
                                        Id = product.Id,
                                        EncryptedId = protector.Protect(product.Id.ToString()),
                                        type1 = type.Name,
                                        Image = product.Image,
                                        Name = product.Name,
                                        Price = product.Price,
                                        Discount = product.Discount,
                                        Description = product.Description,
                                        NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
                                    }).ToList().Skip((page - 1) * numpage).Take(numpage);
                    int countsss;
                    if (pricePro.Count() == 0)
                    {
                        countsss = 0;
                    }
                    else
                    {
                        countsss = (
                       from product in _context.Product.Where(x => x.Price >= minPrice && x.Price <= maxPrice && x.Discount > 0)
                       from cate in _context.Category.Where(c => c.Name == name)
                       join type in _context.Type
                       on cate.Id equals type.IdCategory
                       where product.IdType == type.Id
                       select product).Count();
                    }
                    ViewBag.name = name;
                    ViewBag.total = (Int32)(Math.Ceiling((float)countsss / numpage));
                    ViewBag.currentpage = page;
                    ViewBag.min = minPrice;
                    ViewBag.max = maxPrice;
                    ViewBag.da = data;
                    return View(pricePro);

                }
            }

            if (minPrice != 0 && maxPrice != 0)
            {
                if (data == 3)
                {
                    var pricePro = (from product in _context.Product.Where(x => x.Price >= minPrice && x.Price <= maxPrice && x.Discount > 0)
                                    from cate in _context.Category.Where(c => c.Name == name)
                                    join type in _context.Type
                                    on product.IdType equals type.Id
                                    where type.IdCategory == cate.Id
                                    select new ProductVM
                                    {
                                        Id = product.Id,
                                        EncryptedId = protector.Protect(product.Id.ToString()),
                                        type1 = type.Name,
                                        Image = product.Image,
                                        Name = product.Name,
                                        Price = product.Price,
                                        Discount = product.Discount,
                                        Description = product.Description,
                                        NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
                                    }).ToList().Skip((page - 1) * numpage).Take(numpage);
                    int countsss;
                    if (pricePro.Count() == 0)
                    {
                        countsss = 0;
                    }
                    else
                    {
                        countsss = (
                       from product in _context.Product.Where(x => x.Price >= minPrice && x.Price <= maxPrice && x.Discount > 0)
                       from cate in _context.Category.Where(c => c.Name == name)
                       join type in _context.Type
                       on cate.Id equals type.IdCategory
                       where product.IdType == type.Id
                       select product).Count();
                    }
                    ViewBag.name = name;
                    ViewBag.total = (Int32)(Math.Ceiling((float)countsss / numpage));
                    ViewBag.currentpage = page;
                    ViewBag.min = minPrice;
                    ViewBag.max = maxPrice;
                    ViewBag.da = data;
                    return View(pricePro);

                }
            }

            if (minPrice !=0 && maxPrice !=0 )
            {
               
                var pricePro = (from product in _context.Product.Where(x => x.Price >= minPrice && x.Price <= maxPrice )
                                from cate in _context.Category.Where(c => c.Name == name)
                                join type in _context.Type
                                on product.IdType equals type.Id
                                where type.IdCategory == cate.Id
                                select new ProductVM
                                {
                                    Id = product.Id,
                                    EncryptedId = protector.Protect(product.Id.ToString()),
                                    type1 = type.Name,
                                    Image = product.Image,
                                    Name = product.Name,
                                    Price = product.Price,
                                    Discount = product.Discount,
                                    Description = product.Description,
                                    NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
                                }).ToList().Skip((page - 1) * numpage).Take(numpage);
                int countsss;
                if (pricePro.Count() == 0)
                {
                    countsss = 0;
                }
                else
                {
                    countsss = (
                   from product in _context.Product.Where(x => x.Price >= minPrice && x.Price <= maxPrice)
                   from cate in _context.Category.Where(c => c.Name == name)
                   join type in _context.Type                 
                   on cate.Id equals type.IdCategory                  
                   where product.IdType == type.Id
                   select product).Count();
                }
                ViewBag.name = name;
                ViewBag.total = (Int32)(Math.Ceiling((float)countsss / numpage));
                ViewBag.currentpage = page;
                ViewBag.min = minPrice;
                ViewBag.max = maxPrice;
                ViewBag.da = data;
                return View(pricePro);

            }

       
            if (!String.IsNullOrEmpty(searchString)) {
                var pro1 = (from product in _context.Product.Where(ProductVM => ProductVM.Name.Contains(searchString))
                from cate in _context.Category.Where(c => c.Name == name)
                           join type in _context.Type
                           on product.IdType equals type.Id
                           where type.IdCategory == cate.Id
                           select new ProductVM
                           {
                               Id = product.Id,
                               EncryptedId = protector.Protect(product.Id.ToString()),
                               type1 = type.Name,
                               Image = product.Image,
                               Name = product.Name,
                               Price = product.Price,
                               Discount = product.Discount,
                               Description = product.Description,
                               NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
                           }).ToList().Skip((page - 1) * numpage).Take(numpage);

                int coun;
                if (pro1.Count() == 0)
                {
                    coun = 0;
                }
                else
                {
                    coun = (
                   from product in _context.Product.Where(ProductVM => ProductVM.Name.Contains(searchString))
                   from cate in _context.Category.Where(c => c.Name == name)
                   join type in _context.Type
                     on cate.Id equals type.IdCategory
                   where product.IdType == type.Id
                   select product).Count();
                }
                ViewBag.name = name;
                ViewBag.total = (Int32)(Math.Ceiling((float)coun / numpage));
                ViewBag.currentpage = page;
                ViewBag.search = searchString;

                return View(pro1);

        }


            if (data == 1)
            {
                var pro = (from product in _context.Product
                           from cate in _context.Category.Where(c => c.Name == name)
                           join type in _context.Type
                           on product.IdType equals type.Id
                           where type.IdCategory == cate.Id
                           orderby product.Price
                           select new ProductVM
                           {
                               Id = product.Id,
                               EncryptedId = protector.Protect(product.Id.ToString()),
                               type1 = type.Name,
                               Image = product.Image,
                               Name = product.Name,
                               Price = product.Price,
                               Discount = product.Discount,
                               Description = product.Description,
                               NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
                           }).ToList().Skip((page - 1) * numpage).Take(numpage);
                int coun;
                if (pro.Count() == 0)
                {
                    coun = 0;
                }
                else
                {
                    coun = (
                   from product in _context.Product
                   from cate in _context.Category.Where(c => c.Name == name)
                   join type in _context.Type
                   on cate.Id equals type.IdCategory
                   where product.IdType == type.Id
                   select product).Count();
                }
                ViewBag.name = name;
                ViewBag.total = (Int32)(Math.Ceiling((float)coun / numpage));
                ViewBag.currentpage = page;
                ViewBag.da = data;
                return View(pro);
            }
            if (data == 2)
            {
                var pro = (from product in _context.Product.OrderByDescending(x => x.Price)
                           from cate in _context.Category.Where(c => c.Name == name)
                           join type in _context.Type
                           on product.IdType equals type.Id
                           where type.IdCategory == cate.Id
                           select new ProductVM
                           {
                               Id = product.Id,
                               EncryptedId = protector.Protect(product.Id.ToString()),
                               type1 = type.Name,
                               Image = product.Image,
                               Name = product.Name,
                               Price = product.Price,
                               Discount = product.Discount,
                               Description = product.Description,
                               NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
                           }).ToList().Skip((page - 1) * numpage).Take(numpage);
                int coun;
                if (pro.Count() == 0)
                {
                    coun = 0;
                }
                else
                {
                    coun = (
                   from product in _context.Product
                   from cate in _context.Category.Where(c => c.Name == name)
                   join type in _context.Type
                    on cate.Id equals type.IdCategory
                   where product.IdType == type.Id
                   select product).Count();
                }
                ViewBag.name = name;
                ViewBag.total = (Int32)(Math.Ceiling((float)coun / numpage));
                ViewBag.currentpage = page;
                ViewBag.da = data;
                return View(pro);
            }
            if (data == 3)
            {
                var pro =(
                    from product in _context.Product.Where(x => x.Discount > 0 )
                           from cate in _context.Category.Where(c => c.Name == name)
                           join type in _context.Type
                          on product.IdType equals type.Id
                          where type.IdCategory == cate.Id
                          select new ProductVM
                          {
                              Id = product.Id,
                              EncryptedId = protector.Protect(product.Id.ToString()),
                              type1 = type.Name,
                              Image = product.Image,
                              Name = product.Name,
                              Price = product.Price,
                              Discount = product.Discount,
                              Description = product.Description,
                              NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
                          }).ToList().Skip((page - 1) * numpage).Take(numpage);
                int coun;
                if (pro.Count() == 0)
                {
                    coun= 0;
                }
                else
                {
                    coun = (                    
                   from product in _context.Product.Where(x => x.Discount > 0 )
                   from cate in _context.Category.Where(c => c.Name == name)
                   join type in _context.Type
                    on cate.Id equals type.IdCategory
                   where product.IdType == type.Id
                   select product).Count();
                }
                ViewBag.name = name;
                ViewBag.total = (Int32)(Math.Ceiling((float)coun / numpage));
                ViewBag.currentpage = page;

                ViewBag.da = data;
                return View(pro);
            }


            var listproduct =
                (
                 from cate in _context.Category.Where(c => c.Name == name)
                 from product in _context.Product
                 join type in _context.Type
                 on cate.Id equals type.IdCategory
                 where product.IdType == type.Id
                 select new ProductVM
                 {
                     Id = product.Id,
                     EncryptedId = protector.Protect(product.Id.ToString()),
                     type1 =type.Name,
                     Image = product.Image,
                     Name = product.Name,
                     Price = product.Price,
                     Discount = product.Discount,
                     Description = product.Description,
                     NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
                 }).ToList().Skip((page - 1) * numpage).Take(numpage);
            int count;
            if (listproduct.Count() == 0)
            {
                count = 0;
            }
            else
            {
                count = (
               from product in _context.Product
               from cate in _context.Category.Where(c => c.Name == name)
               join type in _context.Type
               on cate.Id equals type.IdCategory
               where product.IdType == type.Id
               select product).Count();
            }
            ViewBag.name = name;
            ViewBag.total = (Int32)(Math.Ceiling((float)count / numpage));
            ViewBag.currentpage = page;     
            return View(listproduct);
        }


        [HttpGet("Category/{name}/{type}")]
        public IActionResult ProductByType(string name, string type,  int data,string searchString, float minPrice, float maxPrice, int page =1)
        {
            if (minPrice != 0 && maxPrice != 0)
            {
                if (data == 1)
                {
                    var listproduct1 =
               (
               from product in _context.Product.Where(x => x.Price >= minPrice && x.Price <= maxPrice)
               join t in _context.Type
               on product.IdType equals t.Id
               from cate in _context.Category
               where cate.Name == name && t.Name == type
               orderby product.Price
               select new ProductVM
               {
                   Id = product.Id,
                   EncryptedId = protector.Protect(product.Id.ToString()),
                   type1 = type,
                   Image = product.Image,
                   Name = product.Name,
                   Price = product.Price,
                   Discount = product.Discount,
                   Description = product.Description,
                   NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
               }).ToList().Skip((page - 1) * numpage).Take(numpage);
                    var coun = (from product in _context.Product.Where(x => x.Price >= minPrice && x.Price <= maxPrice)
                                join t in _context.Type
                                on product.IdType equals t.Id
                                where t.Name == type
                                select product).Count();
                    ViewBag.name = name;
                    ViewBag.type = type;
                    ViewBag.total = (Int32)(Math.Ceiling((float)coun / numpage));
                    ViewBag.currentpage = page;
                    ViewBag.min = minPrice;
                    ViewBag.max = maxPrice;
                    ViewBag.da = data;
                    return View("Index", listproduct1);

                }
            }
            if (minPrice != 0 && maxPrice != 0)
            {
                if (data == 2)
                {
                    var listproduct1 =
              (
              from product in _context.Product.Where(x => x.Price >= minPrice && x.Price <= maxPrice).OrderByDescending(x =>x.Price)
              join t in _context.Type
              on product.IdType equals t.Id
              from cate in _context.Category
              where cate.Name == name && t.Name == type     
              select new ProductVM
              {
                  Id = product.Id,
                  EncryptedId = protector.Protect(product.Id.ToString()),
                  type1 = type,
                  Image = product.Image,
                  Name = product.Name,
                  Price = product.Price,
                  Discount = product.Discount,
                  Description = product.Description,
                  NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
              }).ToList().Skip((page - 1) * numpage).Take(numpage);
                    var coun = (from product in _context.Product.Where(x => x.Price >= minPrice && x.Price <= maxPrice)
                                join t in _context.Type
                                on product.IdType equals t.Id
                                where t.Name == type
                                select product).Count();
                    ViewBag.name = name;
                    ViewBag.type = type;
                    ViewBag.total = (Int32)(Math.Ceiling((float)coun / numpage));
                    ViewBag.currentpage = page;
                    ViewBag.min = minPrice;
                    ViewBag.max = maxPrice;
                    ViewBag.da = data;
                    return View("Index", listproduct1);

                }
            }

            if (minPrice != 0 && maxPrice != 0)
            {
                if (data == 3)
                {
                    var listproduct1 =
               (
               from product in _context.Product.Where(x => x.Price >= minPrice && x.Price <= maxPrice && x.Discount > 0)
               join t in _context.Type
               on product.IdType equals t.Id
               from cate in _context.Category
               where cate.Name == name && t.Name == type
               orderby product.Price
               select new ProductVM
               {
                   Id = product.Id,
                   EncryptedId = protector.Protect(product.Id.ToString()),
                   type1 = type,
                   Image = product.Image,
                   Name = product.Name,
                   Price = product.Price,
                   Discount = product.Discount,
                   Description = product.Description,
                   NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
               }).ToList().Skip((page - 1) * numpage).Take(numpage);
                    var coun = (from product in _context.Product.Where(x => x.Price >= minPrice && x.Price <= maxPrice && x.Discount > 0)
                                join t in _context.Type
                                on product.IdType equals t.Id
                                where t.Name == type
                                select product).Count();
                    ViewBag.name = name;
                    ViewBag.type = type;
                    ViewBag.total = (Int32)(Math.Ceiling((float)coun / numpage));
                    ViewBag.currentpage = page;
                    ViewBag.min = minPrice;
                    ViewBag.max = maxPrice;
                    ViewBag.da = data;
                    return View("Index", listproduct1);

                }
            }

            if (minPrice != 0 && maxPrice != 0)
            {
                var listproduct1 =
               (
               from product in _context.Product.Where(x => x.Price >= minPrice && x.Price <= maxPrice)
               join t in _context.Type
               on product.IdType equals t.Id
               from cate in _context.Category
               where cate.Name == name && t.Name == type
               select new ProductVM
               {
                   Id = product.Id,
                   EncryptedId = protector.Protect(product.Id.ToString()),
                   type1 = type,
                   Image = product.Image,
                   Name = product.Name,
                   Price = product.Price,
                   Discount = product.Discount,
                   Description = product.Description,
                   NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
               }).ToList().Skip((page - 1) * numpage).Take(numpage);
                var coun = (from product in _context.Product.Where(x => x.Price >= minPrice && x.Price <= maxPrice)
                            join t in _context.Type
                            on product.IdType equals t.Id
                            where t.Name == type
                            select product).Count();
                ViewBag.name = name;
                ViewBag.type = type;
                ViewBag.total = (Int32)(Math.Ceiling((float)coun / numpage));
                ViewBag.currentpage = page;
                ViewBag.min = minPrice;
                ViewBag.max = maxPrice;
                ViewBag.da = data;
                return View("Index", listproduct1);

            }
 

            if (!String.IsNullOrEmpty(searchString))
            {
                var listproduct1 =
              (
              from product in _context.Product.Where(ProductVM => ProductVM.Name.Contains(searchString))
              join t in _context.Type
              on product.IdType equals t.Id
              from cate in _context.Category
              where cate.Name == name && t.Name == type
              select new ProductVM
              {
                  Id = product.Id,
                  EncryptedId = protector.Protect(product.Id.ToString()),
                  type1 = type,
                  Image = product.Image,
                  Name = product.Name,
                  Price = product.Price,
                  Discount = product.Discount,
                  Description = product.Description,
                  NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
              }).ToList().Skip((page - 1) * numpage).Take(numpage);
                var coun = (from product in _context.Product.Where(ProductVM => ProductVM.Name.Contains(searchString))
                            join t in _context.Type
                            on product.IdType equals t.Id
                            where t.Name == type
                            select product).Count();
                ViewBag.name = name;
                ViewBag.type = type;
                ViewBag.total = (Int32)(Math.Ceiling((float)coun / numpage));
                ViewBag.currentpage = page;
                ViewBag.search = searchString;
                ViewBag.da = data;
                return View("Index", listproduct1);
            }
             
            if (data == 1)
            {
                var listproduct1 =
               (
               from product in _context.Product
               join t in _context.Type
               on product.IdType equals t.Id
               from cate in _context.Category
               where cate.Name == name && t.Name == type
               orderby product.Price
               select new ProductVM
               {
                   Id = product.Id,
                   EncryptedId = protector.Protect(product.Id.ToString()),
                   type1 = type,
                   Image = product.Image,
                   Name = product.Name,
                   Price = product.Price,
                   Discount = product.Discount,
                   Description = product.Description,
                   NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
               }).ToList().Skip((page - 1) * numpage).Take(numpage);
                var coun = (from product in _context.Product
                            join t in _context.Type
                            on product.IdType equals t.Id
                            where t.Name == type
                            select product).Count();
                ViewBag.name = name;
                ViewBag.type = type;
                ViewBag.total = (Int32)(Math.Ceiling((float)coun / numpage));
                ViewBag.currentpage = page;
                ViewBag.da = data;
                return View("Index", listproduct1);
            }
                if (data == 2)
            {
                var listproduct2 =
               (
               from product in _context.Product.OrderByDescending(x => x.Price)
               join t in _context.Type
               on product.IdType equals t.Id
               from cate in _context.Category
               where cate.Name == name && t.Name == type
               select new ProductVM
               {
                   Id = product.Id,
                   EncryptedId = protector.Protect(product.Id.ToString()),
                   type1 = type,
                   Image = product.Image,
                   Name = product.Name,
                   Price = product.Price,
                   Discount = product.Discount,
                   Description = product.Description,
                   NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
               }).ToList().Skip((page - 1) * numpage).Take(numpage);
                var coun = (from product in _context.Product
                            join t in _context.Type
                            on product.IdType equals t.Id
                            where t.Name == type
                            select product).Count();
                ViewBag.name = name;
                ViewBag.type = type;
                ViewBag.total = (Int32)(Math.Ceiling((float)coun / numpage));
                ViewBag.currentpage = page;
                ViewBag.da = data;
                return View("Index", listproduct2);
            }
            if (data == 3)
            {
                var listproduct3 =
               (
               from product in _context.Product.Where(x => x.Discount > 0)
               join t in _context.Type
               on product.IdType equals t.Id
               from cate in _context.Category
               where cate.Name == name && t.Name == type
               select new ProductVM
               {
                   Id = product.Id,
                   EncryptedId = protector.Protect(product.Id.ToString()),
                   type1 = type,
                   Image = product.Image,
                   Name = product.Name,
                   Price = product.Price,
                   Discount = product.Discount,
                   Description = product.Description,
                   NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
               }).ToList().Skip((page - 1) * numpage).Take(numpage);
                var coun = (from product in _context.Product.Where(x => x.Discount > 0)
                            join t in _context.Type
                             on product.IdType equals t.Id
                             where t.Name == type
                             select product).Count();
                ViewBag.name = name;
                ViewBag.type = type;
                ViewBag.total = (Int32)(Math.Ceiling((float)coun / numpage));
                ViewBag.currentpage = page;
                ViewBag.da = data;
                return View("Index", listproduct3);
            }
            var listproduct =
               (
               from product in _context.Product
               join t in _context.Type
               on product.IdType equals t.Id
               from cate in _context.Category
               where cate.Name == name && t.Name == type
               select new ProductVM
               {
                   Id = product.Id,
                   EncryptedId = protector.Protect(product.Id.ToString()),
                   type1 = type,
                   Image = product.Image,
                   Name = product.Name,
                   Price = product.Price,
                   Description = product.Description,
                   Discount = product.Discount,
                   NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
               }).ToList().Skip((page - 1) * numpage).Take(numpage);
            var count = (from product in _context.Product
                        join t in _context.Type
                        on product.IdType equals t.Id
                        where t.Name == type
                        select product).Count();
            ViewBag.name = name;
            ViewBag.type = type;
            ViewBag.total = (Int32)(Math.Ceiling((float)count / numpage));
            ViewBag.currentpage = page;
            return View("Index",listproduct);
        }
        [HttpGet("Category/{name}/{type}/Detail/{id}")]
        public ActionResult Detail(string name,string type,string id)
        {
            string decryptedId = protector.Unprotect(id);
            int decryptedIntId = Convert.ToInt32(decryptedId);
            ViewBag.name = name;
            var product = (from p in _context.Product.Where(p => p.Id == decryptedIntId)
                           select new ProductVM
                           {
                               Id = p.Id,
                               EncryptedId = protector.Protect(p.Id.ToString()),
                               type1 = type,
                               Image = p.Image,
                               Name = p.Name,
                               Price = p.Price,
                               Description = p.Description,
                               Discount = p.Discount,
                               NewPrice = (Double)((100 - p.Discount) * p.Price) / 100
                           }).SingleOrDefault();


            var offeritems = _context.Product
                .Where(p => p.IdTypeNavigation.Name == type && p.Id != decryptedIntId)
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
                    Description = product.Description,
                    Discount = product.Discount,
                    NewPrice = (Double)((100 - product.Discount) * product.Price) / 100
                }).Take(6)
                .ToList();
            ViewBag.offeritems = offeritems;
            return View(product);
        }

       
        }


    }



