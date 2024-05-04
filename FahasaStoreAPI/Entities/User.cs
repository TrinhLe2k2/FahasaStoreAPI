using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class User
    {
        public User()
        {
            Addresses = new HashSet<Address>();
            Carts = new HashSet<Cart>();
            Notifications = new HashSet<Notification>();
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
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

        public virtual Role? Role { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
