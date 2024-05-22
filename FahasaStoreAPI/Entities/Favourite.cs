using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Favourite
    {
        public int FavouriteId { get; set; }
        public int? UserId { get; set; }
        public int? BookId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Book? Book { get; set; }
        public virtual User? User { get; set; }
    }
}
