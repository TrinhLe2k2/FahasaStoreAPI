using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Topic
    {
        public Topic()
        {
            TopicContents = new HashSet<TopicContent>();
        }

        public int Id { get; set; }
        public string TopicName { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<TopicContent> TopicContents { get; set; }
    }
}
