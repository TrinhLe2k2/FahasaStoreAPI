using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class PosterImageEntities
    {
        public int PosterImgageId { get; set; }
        public int? BookId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool PosterDefault { get; set; }

        public virtual BookBasic? Book { get; set; }
    }
}
