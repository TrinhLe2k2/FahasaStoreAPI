using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class UserEntities
    {
        public UserEntities()
        {
            Addresses = new HashSet<AddressBasic>();
            Carts = new HashSet<CartBasic>();
            Notifications = new HashSet<NotificationBasic>();
            Orders = new HashSet<OrderBasic>();
            Reviews = new HashSet<ReviewBasic>();
        }

        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }

        public virtual RoleEntities? Role { get; set; }
        public virtual ICollection<AddressBasic> Addresses { get; set; }
        public virtual ICollection<CartBasic> Carts { get; set; }
        public virtual ICollection<NotificationBasic> Notifications { get; set; }
        public virtual ICollection<OrderBasic> Orders { get; set; }
        public virtual ICollection<ReviewBasic> Reviews { get; set; }
    }
}
