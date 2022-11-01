using DTN4.Models;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System.Data.Entity;
using System.Linq;

namespace DTN4.Controllers
{
    public class BlogController : Controller
    {
        private readonly DTNContext _context;

        public BlogController(DTNContext context)
        {
            _context = context;
        }
        [Route("blog.html",Name ="Blog")]
        public IActionResult Index(int? page)
        {

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 4;
            var lsTinTucs = _context.TinTucs
                .AsNoTracking()
                .OrderByDescending(x => x.PostId);
            PagedList<TinTuc> models = new PagedList<TinTuc>(lsTinTucs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }
        [Route("/tin-tuc/{Alias}-{id}.html", Name = "TinDetails")]
        public IActionResult Details(int id)
        {
            var tintuc = _context.TinTucs.AsNoTracking().SingleOrDefault(x => x.PostId == id);
            if (tintuc == null)
            {
                return RedirectToAction("Index");
            }
            var lsBaiVietLienQuan = _context.TinTucs
                .AsNoTracking()
                .Where(x => x.Published == true && x.PostId != id)
                .Take(3).OrderByDescending(x => x.CreateDate)
                .ToList();
            ViewBag.BaiVietLienQuan = lsBaiVietLienQuan;
            return View(tintuc);
        }
    }
}
