using AspNetCoreHero.ToastNotification.Abstractions;
using DTN4.Models;
using DTN4.ModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DTN4.Controllers
{
    public class DonHangController : Controller
    {
        private readonly DTNContext _context;
        public INotyfService _notifyService { get; }
        public DonHangController(DTNContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }
        
        public async Task<IActionResult> Dashboard()
        {
          
            //var taikhoan = HttpContext.Session.GetString("CustomId");
            //if (string.IsNullOrEmpty(taikhoan))
            //    return RedirectToAction("Login", "Accounts");
            //var khachhang = _context.Customers
            //    .AsNoTracking()
            //    .SingleOrDefault(x => x.CustomId == Convert.ToInt32(taikhoan));
            //if (khachhang == null)
            //    return NotFound();
            //var donhang = await _context
            //    .Orders.Include(x => x.TransactStatusId)
            //    .FirstOrDefaultAsync(m => m.OrderId == id && Convert.ToInt32(taikhoan) == m.CustomId);
            //if (donhang == null)
            //{
            //    return NotFound();
            //}
            

            var chitiet = _context.OrderDetails
                .Include(x => x.ProductId)
                .AsNoTracking()
                .Where(x => x.OrderId == 2036)
                .OrderBy(x => x.OrderDetailsId)
                .ToList();
            if (chitiet == null)
            {
                return NotFound();
            }
            else
            {
                return View(chitiet);
            }
            //XemDonHang donHang = new XemDonHang;
            //donHang.
            //return View(chitiet);
            //try
            //{

            //}
            //catch
            //{
            //    return NotFound();
            //}

        }

    }
}
