using DTN4.Models;
using DTN4.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DTN4.Controllers
{
    public class HomeController : Controller
    {
        private readonly DTNContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DTNContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            HomeVM model = new HomeVM();
            var lsProducts = _context.Products.AsNoTracking()
                .Where(x => x.Active == true && x.HomeFlag == true)
                .OrderByDescending(x => x.DateCreate)
                .ToList();
            List<productHomeVM> lsProductViews = new List<productHomeVM>();
            var lsCats = _context.Categories
                .AsNoTracking()
                .Where(x => x.Published == true && x.ParentId == 0)
                .OrderByDescending(x => x.Ordering)
                .ToList();
            foreach (var item in lsCats)
            {
                productHomeVM productHome = new productHomeVM();
                productHome.category = item;
                productHome.lsProducts = lsProducts.Where(x => x.CatId == item.CatId).ToList();
                lsProductViews.Add(productHome);
                var TinTuc = _context.TinTucs
                    .AsNoTracking()
                    .Where(x => x.Published == true && x.IsNewFeed == true)
                    .OrderByDescending(x => x.CreateDate)
                    .Take(3)
                    .ToList();
                model.Products = lsProductViews;
                model.TinTucs = TinTuc;
                ViewBag.AllProducts = lsProducts;
            }
            return View(model);
        }
        [Route("lien-he.html", Name = "Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
