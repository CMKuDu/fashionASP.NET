using AspNetCoreHero.ToastNotification.Abstractions;
using DTN4.Extension;
using DTN4.Models;
using DTN4.ModelViews;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTN4.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly DTNContext _context;
        public INotyfService _notifyService { get; }

        public ShoppingCartController(DTNContext context, INotyfService notifyService)
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
        public async Task<IActionResult> Buy(int id)
        {
            var sp = _context.Products.ToList();
            if (SessionExtensions.Get<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { product = sp.Find(p => p.ProductId == id), amount = 1 });
                SessionExtensions.Set(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItem> cart = SessionExtensions.Get<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].amount++;
                }
                else
                {
                    cart.Add(new CartItem { product = sp.Find(p => p.ProductId == id), amount = 1 });
                }
                SessionExtensions.Set(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index","Product");
        }
        public async Task<IActionResult> Remove(int id)
        {
            List<CartItem> cart = SessionExtensions.Get<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionExtensions.Set(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index","Product");
        }

        private int isExist(int id)
        {
            List<CartItem> cart = SessionExtensions.Get<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].product.ProductId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        //public async Task<IActionResult> CheckOut()
        //{
        //    var cart = SessionExtensions.Get<List<CartItem>>(HttpContext.Session, "cart");
        //    var login = SessionExtensions.Get<List<LoginViewModel>>(HttpContext.Session, "login");
        //    if (login == null)
        //    {

        //        return RedirectToAction("Index", "KhachHangs");


        //    }
        //    else
        //    {
        //        return View(login);
        //    }


        //}
        //public async Task<IActionResult> MuaHang(string email, string diachi, string sdt)
        //{
        //    var cart = SessionExtensions.Get<List<CartItem>>(HttpContext.Session, "cart");
        //    var login = SessionExtensions.Get<List<LoginViewModel>>(HttpContext.Session, "login");
        //    int tien = 35000;
        //    int soluong = 0;
        //    int giamgia = 0;
        //    //foreach (var i in cart)
        //    //{
        //    //    giamgia = ((int?)i.product.Price).Value;
        //    //    tien = tien + giamgia;
        //    //    soluong = soluong + i.amount;
        //    //}

        //    var kh = _context.Customers.First(p => p.Email == email);
        //    Order hd = new Order();
        //    hd.CustomId = kh.CustomId;
        //    hd.ShipDate = DateTime.Now;
        //    hd.Money = tien;
            
        //    //hd.DiaChi = diachi;
        //    //hd.Sdt = sdt;
        //    //hd.ThanhToan = 0;
        //    //hd.VanChuyen = 0;
        //    _context.Orders.Add(hd);
        //    _context.SaveChanges();

        //    int MaHoadon = hd.OrderId;
        //    List<OrderDetail> chitiethoadon = new List<OrderDetail>();
        //    foreach (var item in cart)
        //    {
        //        OrderDetail cthd = new OrderDetail();
        //        cthd.OrderId = MaHoadon;
        //        cthd.ProductId = item.product.ProductId;
        //        cthd.Quantity = item.amount;
        //        cthd.Total = ((int?)item.product.Price).Value;
        //        chitiethoadon.Add(cthd);
        //    }
        //    _context.OrderDetails.AddRange(chitiethoadon);
        //    _context.SaveChanges();
        //    SessionExtensions.Get<List<CartItem>>(HttpContext.Session, "cart").Clear();
        //    List<CartItem> gioHangs = new List<CartItem>();
        //    SessionExtensions.Set(HttpContext.Session, "cart", gioHangs);
        //    return RedirectToAction("Index", "Home");
        //}
    }
}
