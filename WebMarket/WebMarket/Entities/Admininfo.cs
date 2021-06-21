using System;
using System.Collections.Generic;

namespace WebMarket.Entities
{
    public partial class Admininfo
    {
        public Admininfo()
        {
            Order = new HashSet<Order>();
            Orderupdate = new HashSet<Orderupdate>();
            Priceupdate = new HashSet<Priceupdate>();
            Productdetail = new HashSet<Productdetail>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Type { get; set; }

        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Orderupdate> Orderupdate { get; set; }
        public virtual ICollection<Priceupdate> Priceupdate { get; set; }
        public virtual ICollection<Productdetail> Productdetail { get; set; }
    }
}
