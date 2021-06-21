using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using WebMarket.Entities;
using WebMarket.Helpers;
using Type = WebMarket.Entities.Type;

namespace WebMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductManagerController : Controller
    {
        private   WebMarketContext  _context;
        public ProductManagerController(WebMarketContext context)
        {
            _context = context;
        }
        public List<Product> data(int cate = 0, int ty = 0,string status = "Visible")
        {

            var products = (from product in _context.Product
                            join type in _context.Type
                            on product.IdType equals type.Id
                            join category in _context.Category
                            on type.IdCategory equals category.Id
                            where product.Status == status
                            select product);
            if (cate != 0)
            {
                products = (from product in _context.Product
                            join type in _context.Type
                            on product.IdType equals type.Id
                            join category in _context.Category
                            on type.IdCategory equals category.Id
                            where category.Id == cate && product.Status == status
                            select product);
                if (ty != 0)
                {
                    products = (from product in _context.Product
                                join type in _context.Type
                                on product.IdType equals type.Id
                                join category in _context.Category
                                on type.IdCategory equals category.Id
                                where category.Id == cate && type.Id == ty && product.Status == status
                                select product);
                }
            }
            var pro = products.Include(p => p.IdTypeNavigation).Include(c => c.IdProviderNavigation).ToList();
            return pro;
        }
       
        public IActionResult Index(int cate=0,int ty=0, string status = "Visible")
        {
            Category c = new Category()
            {
                Id = 0,
                Name = "All Categories",
            };
            var categories = _context.Category.ToList();
            categories.Insert(0, c);
            ViewBag.IdCategory = new SelectList(categories, "Id", "Name");
            var products = data(cate, ty, status);
            ViewBag.cateId = cate;
            ViewBag.typeId = ty;
            ViewBag.Status = status;
            return View(products);
        }
        public IActionResult filterajax(int cate ,int ty)
        {
            var products = data(cate, ty);
            return PartialView("_filterajax",products);
        }
        
        public IActionResult selectajax(int cate = 0)
        {
            var t = new Type()
            {
                Id = 0,
                Name = "All Types",
            };
            var types = _context.Type.Where(t => t.IdCategory == cate).ToList();
            types.Insert(0, t);
            ViewBag.IdType = new SelectList(types, "Id", "Name");
         
            return PartialView("_selectajax");
        }
        [HttpGet]
        public IActionResult Create()
        {
            var newcate = new Category()
            {
                Id = 0,
                Name = "All Categories",
            };
            var providers = _context.Provider.ToList();
            var types = _context.Type.ToList();
            var cate = _context.Category.ToList();
            cate.Insert(0,newcate);
            ViewBag.IdProvider = new SelectList(providers, "Id", "Name");
            ViewBag.IdType = new SelectList(types, "Id", "Name");
            ViewBag.IdCategory = new SelectList(cate, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product,IFormFile file)
        {
            string image = Helpers.ExtensionHelper.UploadFile(file, "img-products");
            if (image == "error")
            {
                return Content("the image is null ");
            }
            var newproduct = new Product()
            {
                Name = product.Name,
                Discount =product.Discount,
                Price = product.Price,
                Image = image,
                Description = product.Image,
                IdProvider = product.IdProvider,
                IdType = product.IdType
            };
            _context.Product.Add(newproduct);
            _context.SaveChanges();
            int lastRow = _context.Product.OrderByDescending(a => a.Id).Select(a => a.Id).First();
            var user = @User.Claims.FirstOrDefault(c => c.Type == "Ma").Value;
            var updatedetail = new Priceupdate()
            {
                IdProduct = lastRow,
                IdAdmin = Int32.Parse(user),
                Price =(double) product.Price,
                Priceupdated = (double)((100 - product.Discount) * product.Price) / 100,
                DateUpdate = default,
                DateEnd = default,
            };
            _context.Priceupdate.Add(updatedetail);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Product.SingleOrDefault(p => p.Id == id);
            var providers = _context.Provider.ToList();
            var types = _context.Type.ToList();
            
            ViewBag.IdProvider = new SelectList(providers, "Id", "Name");
            ViewBag.IdType = new SelectList(types, "Id", "Name");
            return View(product);
        }
        [HttpPost]
        [Obsolete]
        public IActionResult Edit(Product product,double old_price ,IFormFile file)
        {
            string image = Helpers.ExtensionHelper.UploadFile(file, "img-products");
            if (image != "error")
            {
                product.Image = image;
            }

            var user = @User.Claims.FirstOrDefault(c => c.Type == "Ma").Value;

            if (product.Price != old_price)
            {
                var data = _context.Priceupdate.SingleOrDefault(p => p.IdProduct == product.Id);
                if(data != null)
                {
                    _context.Database.ExecuteSqlCommand("UPDATE [priceupdate] SET priceupdated=" + product.Price + "WHERE ID_product = " + product.Id);
                }
                else
                {
                    var updatedetail = new Priceupdate()
                    {
                        IdProduct = product.Id,
                        IdAdmin = Int32.Parse(user),
                        Price = (double)old_price,
                        Priceupdated = (double)((100 - product.Discount) * product.Price) / 100,
                        DateUpdate = default,
                        DateEnd = default, 
                    };
                    _context.Priceupdate.Add(updatedetail);
                }
                
            }
            _context.Update(product); 
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var product = _context.Product.SingleOrDefault(c => c.Id == id);
            product.Status = "Disable";
            _context.Product.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Removed(int cate=0, int ty=0, string status = "Disable")
        {
            Category c = new Category()
            {
                Id = 0,
                Name = "All Categories",
            };
            var categories = _context.Category.ToList();
            categories.Insert(0, c);
            ViewBag.IdCategory = new SelectList(categories, "Id", "Name");
            var products = data(cate, ty, status);
            ViewBag.cateId = cate;
            ViewBag.typeId = ty;
            ViewBag.Status = status;
            return View(products);
        }
        public IActionResult VisibleStatus(int id)
        {
            var product = _context.Product.SingleOrDefault(c => c.Id == id);
            product.Status = "Visible";
            _context.Product.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Removed");
        }
        public DataTable getData()
        {
            var products = _context.Product.Include(p=>p.IdProviderNavigation).Include(p=>p.IdTypeNavigation).ToList();
            //Creating DataTable  
            DataTable dt = new DataTable();
            //Setiing Table Name  
            dt.TableName = "Provider";
            //Add Columns  
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Discount", typeof(string));
            dt.Columns.Add("Quantity Stock", typeof(string));
            dt.Columns.Add("Quantity Sold", typeof(string));
            dt.Columns.Add("Image", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Provider", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("IdProvider", typeof(string));
            dt.Columns.Add("IdType", typeof(string));
            //Add Rows in DataTable
            foreach (var product in products)
            {
                dt.Rows.Add(product.Id, product.Name, product.Price, product.Discount, product.QuantityStock,
                    product.QuantitySold, product.Image, product.Status, product.Description, product.IdProviderNavigation.Name,
                    product.IdTypeNavigation.Name, product.IdProvider, product.IdType);
            }
            dt.AcceptChanges();
            return dt;
        }
        public ActionResult WriteDataToExcel()
        {
            DataTable dt = getData();
            //Name of File  
            string fileName = "ProductExport.xlsx";
            using (XLWorkbook wb = new XLWorkbook())
            {
                //Add DataTable in worksheet  
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    //Return xlsx Excel File 
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var user = @User.Claims.FirstOrDefault(c => c.Type == "Ma").Value;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;
                    worksheet.Cells.Style.Font.Name = "Arial";
                    worksheet.Cells.Style.Font.Size = 10;
                    for (int row = 2; row <= rowcount; row++)
                    {
                        Product product = new Product();
                        product.Name = worksheet.Cells[row, 2].Value.ToString().Trim();
                        product.Price = double.Parse(worksheet.Cells[row, 3].Value.ToString().Trim());
                        product.Discount = double.Parse(worksheet.Cells[row, 4].Value.ToString().Trim());
                        product.Image = worksheet.Cells[row, 7].Value.ToString().Trim();
                        product.Description = worksheet.Cells[row, 9].Value == null ? string.Empty : worksheet.Cells[row, 9].Value.ToString().Trim();
                        product.IdProvider = int.Parse(worksheet.Cells[row, 12].Value.ToString().Trim());
                        product.IdType = int.Parse(worksheet.Cells[row, 13].Value.ToString().Trim());
                        product.Status = default;
                        _context.Product.Add(product);
                        _context.SaveChanges();
                        int lastRow = _context.Product.OrderByDescending(a => a.Id).Select(a => a.Id).First();
                        var updatedetail = new Priceupdate()
                        {
                            IdProduct = lastRow,
                            IdAdmin = Int32.Parse(user),
                            Price = (double)product.Price,
                            Priceupdated = (double)((100 - product.Discount) * product.Price) / 100,
                            DateUpdate = default,
                            DateEnd = default,
                        };
                        _context.Priceupdate.Add(updatedetail);
                    }
                    _context.SaveChanges();

                }
            }
            return RedirectToAction("Index");
        }
    }
}
