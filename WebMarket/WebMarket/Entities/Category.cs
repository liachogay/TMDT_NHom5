using System;
using System.Collections.Generic;

namespace WebMarket.Entities
{
    public partial class Category
    {
        public Category()
        {
            Type = new HashSet<Type>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Type> Type { get; set; }
    }
}
