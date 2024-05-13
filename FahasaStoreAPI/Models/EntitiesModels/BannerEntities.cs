using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class BannerEntities
    {
        public int BannerId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
