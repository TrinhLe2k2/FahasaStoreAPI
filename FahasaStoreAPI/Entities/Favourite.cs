using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Favourite
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int BookId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual AspNetUser User { get; set; } = null!;
    }
}
