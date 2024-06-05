using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class PosterImage
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public bool ImageDefault { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Book Book { get; set; } = null!;
    }
}
