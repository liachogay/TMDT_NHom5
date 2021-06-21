using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebMarket.Models
{
    public class ProductVM
    {
        public int Id { get; set; }
        [NotMapped]
        public string EncryptedId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string type1 { get; set; }
        public string? category { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public double Discount { get; set; } = 0;
        public double? NewPrice { get; set; } = 0;
        public int? QuantityStock { get; set; } = 0;
    }
}
