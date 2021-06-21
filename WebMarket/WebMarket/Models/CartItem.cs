using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebMarket.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public double Discount { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public double? NewPrice => (((100 - Discount) * Price) / 100);
        public double? TotalPrice => NewPrice * Quantity;

        internal double? Sum(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
