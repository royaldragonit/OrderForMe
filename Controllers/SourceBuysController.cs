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
    public class SourceBuysController: BaseController
    {
        private readonly OrderformeContext _context;

        public SourceBuysController(OrderformeContext context)
        {
            _context = context;
        }

        // GET: SourceBuys
        public async Task<IActionResult> Index()
        {
            return View(await _context.SourceBuy.ToListAsync());
        }

        // GET: SourceBuys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sourceBuy = await _context.SourceBuy
                .FirstOrDefaultAsync(m => m.SourceBuyId == id);
            if (sourceBuy == null)
            {
                return NotFound();
            }

            return View(sourceBuy);
        }

        // GET: SourceBuys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SourceBuys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SourceBuyId,Name,IsActive")] SourceBuy sourceBuy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sourceBuy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sourceBuy);
        }

        // GET: SourceBuys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sourceBuy = await _context.SourceBuy.FindAsync(id);
            if (sourceBuy == null)
            {
                return NotFound();
            }
            return View(sourceBuy);
        }

        // POST: SourceBuys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SourceBuyId,Name,IsActive")] SourceBuy sourceBuy)
        {
            if (id != sourceBuy.SourceBuyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sourceBuy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SourceBuyExists(sourceBuy.SourceBuyId))
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
            return View(sourceBuy);
        }

        // GET: SourceBuys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sourceBuy = await _context.SourceBuy
                .FirstOrDefaultAsync(m => m.SourceBuyId == id);
            if (sourceBuy == null)
            {
                return NotFound();
            }

            return View(sourceBuy);
        }

        // POST: SourceBuys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sourceBuy = await _context.SourceBuy.FindAsync(id);
            _context.SourceBuy.Remove(sourceBuy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SourceBuyExists(int id)
        {
            return _context.SourceBuy.Any(e => e.SourceBuyId == id);
        }
    }
}
