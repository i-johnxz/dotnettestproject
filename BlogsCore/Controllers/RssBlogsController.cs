using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogsCore.Models;

namespace BlogsCore.Controllers
{
    public class RssBlogsController : Controller
    {
        private readonly BloggingContext _context;

        public RssBlogsController(BloggingContext context)
        {
            _context = context;
        }

        // GET: RssBlogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.RssBlogs.ToListAsync());
        }

        // GET: RssBlogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rssBlog = await _context.RssBlogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (rssBlog == null)
            {
                return NotFound();
            }

            return View(rssBlog);
        }

        // GET: RssBlogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RssBlogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RssUrl,BlogId,Name,Url,BlogType,Timestamp")] RssBlog rssBlog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rssBlog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rssBlog);
        }

        // GET: RssBlogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rssBlog = await _context.RssBlogs.FindAsync(id);
            if (rssBlog == null)
            {
                return NotFound();
            }
            return View(rssBlog);
        }

        // POST: RssBlogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RssUrl,BlogId,Name,Url,BlogType,Timestamp")] RssBlog rssBlog)
        {
            if (id != rssBlog.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rssBlog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RssBlogExists(rssBlog.BlogId))
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
            return View(rssBlog);
        }

        // GET: RssBlogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rssBlog = await _context.RssBlogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (rssBlog == null)
            {
                return NotFound();
            }

            return View(rssBlog);
        }

        // POST: RssBlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rssBlog = await _context.RssBlogs.FindAsync(id);
            _context.RssBlogs.Remove(rssBlog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RssBlogExists(int id)
        {
            return _context.RssBlogs.Any(e => e.BlogId == id);
        }
    }
}
