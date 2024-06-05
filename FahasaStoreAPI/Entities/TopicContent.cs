using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class TopicContent
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual Topic Topic { get; set; } = null!;
    }
}
