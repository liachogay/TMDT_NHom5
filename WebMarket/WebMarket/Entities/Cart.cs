using System;
using System.Collections.Generic;

namespace WebMarket.Entities
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int IdCustomer { get; set; }
        public int Quantity { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
