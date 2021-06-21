using System;
using System.Collections.Generic;

namespace WebMarket.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }

        public virtual Account IdNavigation { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
