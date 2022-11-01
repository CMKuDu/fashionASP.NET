using System;
using System.Collections.Generic;

namespace DTN4.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomId { get; set; }
        public string CustomName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? LocationId { get; set; }
        public int? District { get; set; }
        public DateTime? Create { get; set; }
        public string Salt { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool? Active { get; set; }
        public int? Word { get; set; }
        public string Password { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
