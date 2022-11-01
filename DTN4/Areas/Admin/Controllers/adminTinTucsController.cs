using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DTN4.Models;
using PagedList.Core;
using DTN4.Helpper;
using System.IO;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace DTN4.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class adminTinTucsController : Controller
    {
        private readonly DTNContext _context;
        public INotyfService _notifyService { get; }

        public adminTinTucsController(DTNContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/adminTinTucs
        public IActionResult Index(int? page)
        {
            var collection = _context.TinTucs.AsNoTracking().ToList();
            foreach (var item in collection)
            {
                if (item.CreateDate == null)
                {
                    item.CreateDate = DateTime.Now;
                    _context.Add(item);
                    _context.SaveChanges();
                }
            }


            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            var lsTinTucs = _context.TinTucs
                .AsNoTracking()
                .OrderByDescending(x => x.PostId);

            PagedList<TinTuc> models = new PagedList<TinTuc>(lsTinTucs, pageNumber, pageSize);

            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/adminTinTucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TinTucs == null)
            {
                return NotFound();
            }

            var tinTuc = await _context.TinTucs
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }

        // GET: Admin/adminTinTucs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/adminTinTucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,Scontenct,Thumb,Published,Alias,CreateDate,Author,AccountId,Tags,CatId,IsNewFeed,MetaKey,MetaDesc,View,IsHot")] TinTuc tinTuc, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Untilities.SEOUrl(tinTuc.Title) + extension;
                        tinTuc.Thumb = await Untilities.UploadFile(fThumb, @"news", image.ToLower());

                    }
                    if (string.IsNullOrEmpty(tinTuc.Thumb)) tinTuc.Thumb = "default.jbg";
                    tinTuc.Alias = Untilities.SEOUrl(tinTuc.Title);
                    _context.Update(tinTuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TinTucExists(tinTuc.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tinTuc);
        }

        // GET: Admin/adminTinTucs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TinTucs == null)
            {
                return NotFound();
            }

            var tinTuc = await _context.TinTucs.FindAsync(id);
            if (tinTuc == null)
            {
                return NotFound();
            }
            return View(tinTuc);
        }

        // POST: Admin/adminTinTucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Scontenct,Thumb,Published,Alias,CreateDate,Author,AccountId,Tags,CatId,IsNewFeed,MetaKey,MetaDesc,View,IsHot")] TinTuc tinTuc, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != tinTuc.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Untilities.SEOUrl(tinTuc.Title) + extension;
                        tinTuc.Thumb = await Untilities.UploadFile(fThumb, @"news", image.ToLower());
                        
                    }
                    if (string.IsNullOrEmpty(tinTuc.Thumb)) tinTuc.Thumb = "default.jbg";
                    tinTuc.Alias = Untilities.SEOUrl(tinTuc.Title);
                    _context.Update(tinTuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TinTucExists(tinTuc.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tinTuc);
        }

        // GET: Admin/adminTinTucs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TinTucs == null)
            {
                return NotFound();
            }

            var tinTuc = await _context.TinTucs
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }

        // POST: Admin/adminTinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TinTucs == null)
            {
                return Problem("Entity set 'DTNContext.TinTucs'  is null.");
            }
            var tinTuc = await _context.TinTucs.FindAsync(id);
            if (tinTuc != null)
            {
                _context.TinTucs.Remove(tinTuc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TinTucExists(int id)
        {
            return _context.TinTucs.Any(e => e.PostId == id);
        }
    }
}
