using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public string Username { get; set; } = null!;
        public int? UserId { get; set; }
        public string? Password { get; set; }
        public bool Active { get; set; }

        public virtual User? User { get; set; }
    }
}
