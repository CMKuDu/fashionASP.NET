using DTN4.Models;
using System.Collections.Generic;

namespace DTN4.ModelViews
{
    public class productHomeVM
    {
        public Category category { get; set; }
        public List<Product> lsProducts { get; set; }
    }
}
