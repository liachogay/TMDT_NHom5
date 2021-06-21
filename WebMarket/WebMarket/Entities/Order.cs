using System;
using System.Collections.Generic;

namespace WebMarket.Entities
{
    public partial class Order
    {
        public Order()
        {
            Orderdetail = new HashSet<Orderdetail>();
            Orderupdate = new HashSet<Orderupdate>();
        }

        public int Id { get; set; }
        public int IdCustomer { get; set; }
        public int? IdAdmin { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PaymentType { get; set; }
        public string ShippingType { get; set; }
        public double? ShipCost { get; set; }
        public int? Status { get; set; }
        public string Note { get; set; }
        public double? TotalPrice { get; set; }

        public virtual Admininfo IdAdminNavigation { get; set; }
        public virtual Customer IdCustomerNavigation { get; set; }
        public virtual ICollection<Orderdetail> Orderdetail { get; set; }
        public virtual ICollection<Orderupdate> Orderupdate { get; set; }
    }
}
