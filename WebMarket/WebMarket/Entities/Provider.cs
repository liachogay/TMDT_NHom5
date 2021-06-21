using System;
using System.Collections.Generic;

namespace WebMarket.Entities
{
    public partial class Provider
    {
        public Provider()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
