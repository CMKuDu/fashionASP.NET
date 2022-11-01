using System;
using System.Collections.Generic;

namespace DTN4.Models
{
    public partial class Product
    {
        public Product()
        {
            AttributesPrices = new HashSet<AttributesPrice>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ShortDesc { get; set; }
        public string Description { get; set; }
        public int? CatId { get; set; }
        public int? Price { get; set; }
        public int? Discount { get; set; }
        public string Thumb { get; set; }
        public string Video { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? BestSeller { get; set; }
        public bool? HomeFlag { get; set; }
        public bool? Active { get; set; }
        public string Tags { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string MetaKey { get; set; }
        public int? UntiStock { get; set; }

        public virtual Category Cat { get; set; }
        public virtual ICollection<AttributesPrice> AttributesPrices { get; set; }
    }
}
