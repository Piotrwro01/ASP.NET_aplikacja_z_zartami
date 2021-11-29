using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Core_Course.Data;
using ASP.NET_Core_Course.Models;

namespace ASP.NET_Core_Course.Controllers
{
    public class KawalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KawalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kawals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kawal.ToListAsync());
        }

        // GET: Kawals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kawal = await _context.Kawal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kawal == null)
            {
                return NotFound();
            }

            return View(kawal);
        }

        // GET: Kawals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kawals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PytanieKawalu,OdpowiedzKawalu")] Kawal kawal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kawal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kawal);
        }

        // GET: Kawals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kawal = await _context.Kawal.FindAsync(id);
            if (kawal == null)
            {
                return NotFound();
            }
            return View(kawal);
        }

        // POST: Kawals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PytanieKawalu,OdpowiedzKawalu")] Kawal kawal)
        {
            if (id != kawal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kawal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KawalExists(kawal.Id))
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
            return View(kawal);
        }

        // GET: Kawals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kawal = await _context.Kawal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kawal == null)
            {
                return NotFound();
            }

            return View(kawal);
        }

        // POST: Kawals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kawal = await _context.Kawal.FindAsync(id);
            _context.Kawal.Remove(kawal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KawalExists(int id)
        {
            return _context.Kawal.Any(e => e.Id == id);
        }
    }
}
