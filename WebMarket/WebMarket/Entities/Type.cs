using System;
using System.Collections.Generic;

namespace WebMarket.Entities
{
    public partial class Type
    {
        public Type()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdCategory { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
