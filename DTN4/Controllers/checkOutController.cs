using AspNetCoreHero.ToastNotification.Abstractions;
using DTN4.Extension;
using DTN4.Models;
using DTN4.ModelViews;
using Google.Apis.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SessionExtensions = DTN4.Extension.SessionExtensions;

namespace DTN4.Controllers
{
    public class checkOutController : Controller
    {
        private readonly DTNContext _context;
        public INotyfService _notifyService { get; }

        public checkOutController(DTNContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }
        public async Task<IActionResult> Index()
        {


            if (SessionExtensions.Get<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                return View(cart);
            }
            else
            {
                var cart = SessionExtensions.Get<List<CartItem>>(HttpContext.Session, "cart").ToList();
                return View(cart);
            }

        }
        public IActionResult MuaHang()
        {
            var Cart = HttpContext.Session.Get<List<CartItem>>("cart");
            var taikhoanID = HttpContext.Session.GetString("CustomerID");
            muaHang model = new muaHang();
            if(taikhoanID != null)
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomId == Convert.ToInt32(taikhoanID));
                model.CustomerID = khachhang.CustomId;
                model.fullName = khachhang.CustomName;
                model.Phone = khachhang.Phone;
                model.Email = khachhang.Email;
                model.Address = khachhang.Address;
                


            }
            //ViewData["lsTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1).OrderBy(x => x.Type).ToList(), "LocationId", "Name");
            ViewBag.cart = Cart;    
            return View(model);
        }
        public IActionResult Index(muaHang muaHang)
        {
            var Cart = SessionExtensions.Get<List<CartItem>>(HttpContext.Session,"cart");
            var taikhoanID = HttpContext.Session.GetString("CustomerID");
            muaHang model = new muaHang();
            if (taikhoanID != null)
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomId == Convert.ToInt32(taikhoanID));
                
                model.CustomerID = khachhang.CustomId;
                model.fullName = khachhang.CustomName;
                model.Phone = khachhang.Phone;
                model.Email = khachhang.Email;
                model.Address = khachhang.Address; 

                //khachhang.Lo = muaHang.TinhThanh;
                khachhang.District = muaHang.QuanHuyen;
                khachhang.Word = muaHang.ThiXa;
                khachhang.Address = muaHang.Address;
                _context.Update(khachhang);
                _context.SaveChanges();
            }
            try
            {
                if(ModelState.IsValid)
                {
                    Order donhang = new Order();
                    donhang.CustomId = model.CustomerID;
                    donhang.Custom.Address = model.Address;
                    donhang.Custom.District = model.QuanHuyen;
                    donhang.Custom.Word = model.ThiXa;

                    //donhang.OrderDate = DateTime.Now;
                    donhang.TransactStatusId = 1;
                    donhang.Delete = false;
                    donhang.Paid = false;
                    //donhang.Note = Utilities.StripHTML(model.Note);
                    donhang.Money = Convert.ToInt32(Cart.Sum(x => x.TotalMoney));
                    _context.Update(donhang);
                    _context.SaveChanges();
                    foreach (var item in Cart)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderId = donhang.OrderId;
                        orderDetail.ProductId = item.product.ProductId;
                        //orderDetail. = item.amount;
                        orderDetail.Total = donhang.Money;             
                        orderDetail.Order.PayDate = DateTime.Now;
                        _context.Add(orderDetail);

                    }
                    _context.SaveChanges();
                    HttpContext.Session.Remove("cart");
                    _notifyService.Success("Đơn hàng đặt thành công");
                    return RedirectToAction("Success");
                }
                
            }
            catch(Exception ex)
            {
                ViewBag.GioHang = Cart;
                return View(model);
            }
            ViewBag.GioHang = Cart;
            return View(model);            
        }
        //[Route("dat-hang-thanh-cong.html", Name = "Success")]
        //public IActionResult Success()
        //{
        //    try
        //    {
        //        var taikhoanID = HttpContext.Session.GetString("CustomerId");
        //        if(string.IsNullOrEmpty(taikhoanID))
        //        {
        //            return RedirectToAction("Login","Accounts", new {returnUrl="/dat-hang-thanh-cong.hmlt"});
        //        }
        //        var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomId == Convert.ToInt32(taikhoanID));
        //        var khachang = _context.Orders.Where(x => x.CustomId == Convert.ToInt32(taikhoanID)).OrderByDescending(x => x.OrderDate);

        //    }
        //    catch
        //    {

        //    }
        //}
       
    }
}
