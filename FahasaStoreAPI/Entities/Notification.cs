using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public int? NotificationTypeId { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }

        public virtual NotificationType? NotificationType { get; set; }
        public virtual User? User { get; set; }
    }
}
