using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMarket.Areas.Admin.Models
{
    public class AdminVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime date { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string type { get; set; }
    }
}
