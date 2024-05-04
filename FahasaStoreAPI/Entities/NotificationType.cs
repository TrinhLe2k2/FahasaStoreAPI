using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class NotificationType
    {
        public NotificationType()
        {
            Notifications = new HashSet<Notification>();
        }

        public int NotificationTypeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
