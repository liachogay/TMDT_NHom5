using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using WebMarket.Entities;

namespace WebMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProviderManagerController : Controller
    {
        private WebMarketContext _context;
        public ProviderManagerController(WebMarketContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var providers = _context.Provider.ToList();
            return View(providers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Provider provider)
        {
            var newprovider = new Provider()
            {
                Name=provider.Name,
                Address = provider.Address,
                Phone = provider.Phone,
            };
            _context.Provider.Add(newprovider);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var provider = _context.Provider.SingleOrDefault(t => t.Id == id);
            return View(provider);
        }
        [HttpPost]
        public IActionResult Edit(Provider provider)
        {
            
            _context.Provider.Update(provider);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var provider = _context.Provider.SingleOrDefault(c => c.Id == id);
            _context.Provider.Remove(provider);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public DataTable getData()
        {
            var providers = _context.Provider.ToList();
            //Creating DataTable  
            DataTable dt = new DataTable();
            //Setiing Table Name  
            dt.TableName = "Provider";
            //Add Columns  
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            //Add Rows in DataTable
            foreach (var provider in providers)
            {
                dt.Rows.Add(provider.Id,provider.Name, provider.Address, provider.Phone);
            }
            dt.AcceptChanges();
            return dt;
        }
        public ActionResult WriteDataToExcel()
        {
            DataTable dt = getData();
            //Name of File  
            string fileName = "ProviderExport.xlsx";
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
            
            using ( var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowcount; row++)
                    {
                        Provider provider = new Provider();
                        provider.Name = worksheet.Cells[row, 2].Value.ToString().Trim();
                        _context.Provider.Add(provider);
                       
                    }
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}

