using DTN4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;

namespace DTN4.Controllers
{
    public class PageController : Controller
    {

        private readonly DTNContext _context;

        public PageController(DTNContext context)
        {
            _context = context;
        }
        
     
        [Route("/page/{Alias}", Name = "PageDetails")]
        public IActionResult Details(string Alias)
        {
            if(string.IsNullOrEmpty(Alias)) return RedirectToAction("Index", "Home");
            var page = _context.Pages.AsNoTracking().SingleOrDefault(x => x.Alias == Alias);
            if (page == null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View(page);
        }
    }
}
