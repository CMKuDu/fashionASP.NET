using DTN4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DTN4.Controllers
{
    public class ProductController : Controller
    {
        private readonly DTNContext _context;

        public ProductController(DTNContext context)
        {
            _context = context;
        }
        [Route("shop.html", Name = "ShopProduct")]
        public IActionResult Index(string searchString,int? page)
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 4;
                var lsTinTucs = _context.Products
                    .AsNoTracking()
                    .OrderByDescending(x => x.DateCreate);
                PagedList<Product> models = new PagedList<Product>(lsTinTucs, pageNumber, pageSize);
                ViewBag.CurrentPage = pageNumber;
                return View(models);

            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        [Route("/{Alias}", Name = "ListProduct")]
        public IActionResult List(string Alias, int page = 1)
        {
            try
            {
                var pageSize = 6;
                var danhmuc = _context.Categories.AsNoTracking().SingleOrDefault(x => x.Alias == Alias);
                var lsTinTucs = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CatId == danhmuc.CatId)
                    .OrderByDescending(x => x.DateCreate);
                PagedList<Product> models = new PagedList<Product>(lsTinTucs, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.CurrentCat = danhmuc;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index","Home");
            }
            
            
        }
        [Route("/{Alias}-{id}.html", Name = "ProductDetails")]
        public IActionResult Details(int id)
        {
            try
            {
                var product = _context.Products.Include(x => x.Cat).FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }
                var lsProduct = _context.Products.AsNoTracking()
                    .Where(x => x.CatId == product.CatId && x.ProductId != id && x.Active == true)
                    .OrderByDescending(x => x.DateCreate)
                    .Take(3)
                    .ToList();
                ViewBag.Sanpham = lsProduct;

                return View(product);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        public async Task<IActionResult> Search(string daudeptrai, int? page)
        {

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 6;
            var lsTinTucs = _context.Products.Where(p => p.ProductName.Contains(daudeptrai));
            PagedList<Product> models = new PagedList<Product>(lsTinTucs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;


            if (daudeptrai == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Index", models);
            }
            

           
        }
    }
}
