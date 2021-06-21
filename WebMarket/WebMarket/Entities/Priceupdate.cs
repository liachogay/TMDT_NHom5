using System;
using System.Collections.Generic;

namespace WebMarket.Entities
{
    public partial class Priceupdate
    {
        public int Id { get; set; }
        public int? IdProduct { get; set; }
        public int IdAdmin { get; set; }
        public double Price { get; set; }
        public double Priceupdated { get; set; }
        public DateTime DateUpdate { get; set; }
        public DateTime DateEnd { get; set; }

        public virtual Admininfo IdAdminNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
