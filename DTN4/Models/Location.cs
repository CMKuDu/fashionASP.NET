using System;
using System.Collections.Generic;

namespace DTN4.Models
{
    public partial class Location
    {
        public Location()
        {
            Customers = new HashSet<Customer>();
        }

        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Type { get; set; }
        public string Slup { get; set; }
        public string PathNwithType { get; set; }
        public int? ParentCode { get; set; }
        public int? Levels { get; set; }
        public string NameWithType { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
