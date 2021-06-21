using System;
using System.Collections.Generic;

namespace WebMarket.Entities
{
    public partial class Warehouse
    {
        public int Id { get; set; }
        public int IdAdmin { get; set; }

        public virtual Admininfo IdAdminNavigation { get; set; }
    }
}
