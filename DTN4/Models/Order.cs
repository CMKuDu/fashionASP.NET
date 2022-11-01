using System;
using System.Collections.Generic;

namespace DTN4.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int? CustomId { get; set; }
        public string OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public int? TransactStatusId { get; set; }
        public bool? Delete { get; set; }
        public bool? Paid { get; set; }
        public DateTime? PayDate { get; set; }
        public int? PaymentId { get; set; }
        public int? Note { get; set; }
        public int Money { get; set; }

        public virtual Customer Custom { get; set; }
        public virtual TransactStatus TransactStatus { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
