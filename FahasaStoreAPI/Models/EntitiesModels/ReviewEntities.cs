using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class ReviewEntities
    {
        public int ReviewId { get; set; }
        public int? BookId { get; set; }
        public int? UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        public virtual BookBasic? Book { get; set; }
        public virtual UserBasic? User { get; set; }
    }
}
