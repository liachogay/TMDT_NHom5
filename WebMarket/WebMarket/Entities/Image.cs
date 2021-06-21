using System;
using System.Collections.Generic;

namespace WebMarket.Entities
{
    public partial class Image
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public string Image1 { get; set; }

        public virtual Product IdProductNavigation { get; set; }
    }
}
