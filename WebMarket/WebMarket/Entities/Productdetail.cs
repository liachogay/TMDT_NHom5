using System;
using System.Collections.Generic;

namespace WebMarket.Entities
{
    public partial class Productdetail
    {
        public int Id { get; set; }
        public int? IdWarehouse { get; set; }
        public int IdProduct { get; set; }
        public int IdAdmin { get; set; }
        public int Quantity { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime Mfg { get; set; }
        public DateTime Exp { get; set; }

        public virtual Admininfo IdAdminNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
