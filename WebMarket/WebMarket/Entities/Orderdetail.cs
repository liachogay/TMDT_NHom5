using System;
using System.Collections.Generic;

namespace WebMarket.Entities
{
    public partial class Orderdetail
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }
        public double? Newprice { get; set; }

        public virtual Order IdOrderNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
