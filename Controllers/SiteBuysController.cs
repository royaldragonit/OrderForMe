using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderForMeProject.Models.Entities;

namespace OrderForMeProject.Controllers
{
    public class SiteBuysController: BaseController
    {
        private readonly OrderformeContext _context;

        public SiteBuysController(OrderformeContext context)
        {
            _context = context;
        }

        // GET: SiteBuys
        public async Task<IActionResult> Index()
        {
            return View(await _context.SiteBuy.ToListAsync());
        }

        // GET: SiteBuys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteBuy = await _context.SiteBuy
                .FirstOrDefaultAsync(m => m.SiteBuyId == id);
            if (siteBuy == null)
            {
                return NotFound();
            }

            return View(siteBuy);
        }

        // GET: SiteBuys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SiteBuys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiteBuyId,Name,IsActive")] SiteBuy siteBuy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siteBuy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(siteBuy);
        }

        // GET: SiteBuys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteBuy = await _context.SiteBuy.FindAsync(id);
            if (siteBuy == null)
            {
                return NotFound();
            }
            return View(siteBuy);
        }

        // POST: SiteBuys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SiteBuyId,Name,IsActive")] SiteBuy siteBuy)
        {
            if (id != siteBuy.SiteBuyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siteBuy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteBuyExists(siteBuy.SiteBuyId))
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
            return View(siteBuy);
        }

        // GET: SiteBuys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteBuy = await _context.SiteBuy
                .FirstOrDefaultAsync(m => m.SiteBuyId == id);
            if (siteBuy == null)
            {
                return NotFound();
            }

            return View(siteBuy);
        }

        // POST: SiteBuys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siteBuy = await _context.SiteBuy.FindAsync(id);
            _context.SiteBuy.Remove(siteBuy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteBuyExists(int id)
        {
            return _context.SiteBuy.Any(e => e.SiteBuyId == id);
        }
    }
}
