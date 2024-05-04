using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class PosterImage
    {
        public int PosterImgageId { get; set; }
        public int? BookId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool PosterDefault { get; set; }

        public virtual Book? Book { get; set; }
    }
}
