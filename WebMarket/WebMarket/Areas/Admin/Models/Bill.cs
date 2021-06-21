using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMarket.Entities;

namespace WebMarket.Areas.Admin.Models
{
    public class Bill
    {
        public Order order { get; set; }
        public List<Orderdetail> orderdetail { get; set; }
    }
}
