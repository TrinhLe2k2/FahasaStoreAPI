using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class User
    {
        public User()
        {
            Accounts = new HashSet<Account>();
            Addresses = new HashSet<Address>();
            Favourites = new HashSet<Favourite>();
            Notifications = new HashSet<Notification>();
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
        }

        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? PublicId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Active { get; set; }

        public virtual Role? Role { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
