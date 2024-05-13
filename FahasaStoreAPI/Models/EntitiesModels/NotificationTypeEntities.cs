using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class NotificationTypeEntities
    {
        public NotificationTypeEntities()
        {
            Notifications = new HashSet<NotificationBasic>();
        }

        public int NotificationTypeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<NotificationBasic> Notifications { get; set; }
    }
}
