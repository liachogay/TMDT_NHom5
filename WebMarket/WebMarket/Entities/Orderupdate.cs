using System;
using System.Collections.Generic;

namespace WebMarket.Entities
{
    public partial class Orderupdate
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int? IdAdmin { get; set; }
        public int? OldStatus { get; set; }
        public int? NewStatus { get; set; }
        public DateTime DateUpdate { get; set; }
        public DateTime DateEnd { get; set; }

        public virtual Admininfo IdAdminNavigation { get; set; }
        public virtual Order IdOrderNavigation { get; set; }
    }
}
