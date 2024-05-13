using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class NotificationEntities
    {
        public int NotificationId { get; set; }
        public int? NotificationTypeId { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }

        public virtual NotificationTypeBasic? NotificationType { get; set; }
        public virtual UserBasic? User { get; set; }
    }
}
