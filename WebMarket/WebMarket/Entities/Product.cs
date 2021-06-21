using System;
using System.Collections.Generic;

namespace WebMarket.Entities
{
    public partial class Product
    {
        public Product()
        {
            Orderdetail = new HashSet<Orderdetail>();
            Priceupdate = new HashSet<Priceupdate>();
            Productdetail = new HashSet<Productdetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int? IdProvider { get; set; }
        public int IdType { get; set; }
        public double Discount { get; set; }
        public int? QuantityStock { get; set; }
        public int? QuantitySold { get; set; }
        public string Status { get; set; }

        public virtual Provider IdProviderNavigation { get; set; }
        public virtual Type IdTypeNavigation { get; set; }
        public virtual ICollection<Orderdetail> Orderdetail { get; set; }
        public virtual ICollection<Priceupdate> Priceupdate { get; set; }
        public virtual ICollection<Productdetail> Productdetail { get; set; }
    }
}
