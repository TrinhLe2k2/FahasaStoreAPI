using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Favourite
    {
        public int FavouriteId { get; set; }
        public string? UserId { get; set; }
        public int? BookId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Book? Book { get; set; }
        public virtual AspNetUser? User { get; set; }
    }
}
