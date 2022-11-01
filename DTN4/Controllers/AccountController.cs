using AspNetCoreHero.ToastNotification.Abstractions;
using DTN4.Extension;
using DTN4.Helpper;
using DTN4.Models;
using DTN4.ModelViews;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SessionExtensions = DTN4.Extension.SessionExtensions;

// Persistence session on ASP

namespace DTN4.Controllers
{
    
    public class AccountController : Controller
    {
        
        private readonly DTNContext _context;
        public INotyfService _notifyService { get; }

        public AccountController(DTNContext context, INotyfService  notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidatePhone(string Phone)
        {
            try
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Phone.ToLower() == Phone.ToLower());
                if (khachhang != null)
                    return Json(data: "Số điện thoai: " + Phone + "Đã được sử dung");
                return Json(data: true);
            }
            catch
            {
                return Json(data: true);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidateEmail(string Email)
        {
            try
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == Email.ToLower());
                if (khachhang != null)
                    return Json(data: "Email: " + Email + "Đã được sử dung<br />");
                return Json(data: true);
            }
            catch
            {
                return Json(data: true);
            }
        }

        /* public IActionResult Index()
         {
             return View();
         }*/
        [HttpGet]
        [Route("tai-khoan-cua-toi.html", Name = "Dashboard")]
        public IActionResult Dashboard()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomId");
            if (taikhoanID != null)
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomId == Convert.ToInt32(taikhoanID));
                if (khachhang != null)
                {
                    var lsDonHang = _context
                        .Orders
                        .Include(x => x.TransactStatus)
                        .AsNoTracking()
                        .Where(x => x.CustomId == khachhang.CustomId)
                        .OrderByDescending(x => x.OrderDate)
                        .ToList();
                    ViewBag.lsDonHang = lsDonHang;
                    return View(khachhang);
                }
            }
            return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //[Route("dang-ky.html", Name = "Dangky")]
        public async Task<IActionResult> Dangky(RegisterViewModel taikhoan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string salt = Untilities.GetRandomKey();
                    Customer khachhang = new Customer
                    {
                        CustomName = taikhoan.fullName,
                        Phone = taikhoan.Phone.Trim().ToLower(),
                        Email = taikhoan.Email.Trim().ToLower(),
                        Password = (taikhoan.Password + salt.Trim()).ToMD5(),
                        Active = true,
                        Salt = salt,
                        Create = DateTime.Now

                    };
                    try
                    {
                        _context.Add(khachhang);
                        await _context.SaveChangesAsync();
                        HttpContext.Session.SetString("CustomId", khachhang.CustomId.ToString());
                        var taikhoanID = HttpContext.Session.GetString("CustomId");
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, khachhang.CustomName),
                            new Claim("CustomId",khachhang.CustomId.ToString())
                        };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);

                        return RedirectToAction("Dashboard", "Account");
                    }
                    catch
                    {
                        return RedirectToAction("Dangky", "Account");
                    }
                }
                else
                {
                    return View(taikhoan);
                }
            }
            catch
            {
                return View(taikhoan);
            }
        }

        [HttpGet]
        public IActionResult index(string returnUrl = null)
        {
            var taikhoanID = HttpContext.Session.GetString("CustomId");
            if (taikhoanID != null)
            {

                return RedirectToAction("Dashboard", "Account");
            }

            return View();
        }

        [HttpPost]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public async Task<IActionResult> Login(LoginViewModel customer)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == customer.UserName);
                    if (khachhang == null)
                    {
                        return RedirectToAction("Dangky");

                    }
                    else
                    {
                        string pass = (customer.Password + khachhang.Salt.Trim()).ToMD5();
                        if (khachhang.Password != pass)
                        {
                            _notifyService.Success("Sai thông tin đăng nhập!");
                            return View("Index");
                        }
                        else
                        {
                            /*if (khachhang.Active == false)
                            {
                                return RedirectToAction("ThongBao", "Account");
                            }*/
                            await _context.SaveChangesAsync();
                            HttpContext.Session.SetString("CustomId", khachhang.CustomId.ToString());
                            var taikhoanID = HttpContext.Session.GetString("CustomId");
                            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, khachhang.CustomName),
                            new Claim("CustomId",khachhang.CustomId.ToString())
                        };
                            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            await HttpContext.SignInAsync(claimsPrincipal);
                            _notifyService.Success("Đăng nhập thành công");
                            return RedirectToAction("Dashboard", "Account");
                        }


                    }
                }
                else
                {
                    return RedirectToAction("index", "Account");
                }
            }
            catch
            {
                return RedirectToAction("Dangky", "Account");
            }
            return View(customer);
        }

        [HttpGet]
        [Route("dang-xuat.html", Name = "Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("CustomId");
            return RedirectToAction("Index", "Home");
        }
          
    }  
}

