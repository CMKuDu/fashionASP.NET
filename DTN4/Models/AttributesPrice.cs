using System;
using System.Collections.Generic;

namespace DTN4.Models
{
    public partial class AttributesPrice
    {
        public int AttributesPriceId { get; set; }
        public int? AttributesId { get; set; }
        public int? ProductId { get; set; }
        public bool? Active { get; set; }

        public virtual Attribute Attributes { get; set; }
        public virtual Product Product { get; set; }
    }
}
