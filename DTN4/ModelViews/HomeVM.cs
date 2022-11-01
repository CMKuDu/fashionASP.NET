using DTN4.Models;
using System.Collections.Generic;

namespace DTN4.ModelViews
{
    public class HomeVM
    {
        public List<productHomeVM> Products { get; set; }
        public List<TinTuc> TinTucs { get; set; }
        //public QuangCao quangcao { get;set }
    }
}
