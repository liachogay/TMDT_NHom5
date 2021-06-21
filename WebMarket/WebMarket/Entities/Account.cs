using System;
using System.Collections.Generic;

namespace WebMarket.Entities
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
