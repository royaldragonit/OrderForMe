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
    public class GroupProductsController: BaseController
    {
        private readonly OrderformeContext _context;

        public GroupProductsController(OrderformeContext context)
        {
            _context = context;
        }

        // GET: GroupProducts
        public async Task<IActionResult> Index()
        {
            return View(await _context.GroupProduct.ToListAsync());
        }

        // GET: GroupProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupProduct = await _context.GroupProduct
                .FirstOrDefaultAsync(m => m.GroupProductId == id);
            if (groupProduct == null)
            {
                return NotFound();
            }

            return View(groupProduct);
        }

        // GET: GroupProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GroupProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupProductId,Name,Price,IsActive")] GroupProduct groupProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(groupProduct);
        }

        // GET: GroupProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupProduct = await _context.GroupProduct.FindAsync(id);
            if (groupProduct == null)
            {
                return NotFound();
            }
            return View(groupProduct);
        }

        // POST: GroupProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupProductId,Name,Price,IsActive")] GroupProduct groupProduct)
        {
            if (id != groupProduct.GroupProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupProductExists(groupProduct.GroupProductId))
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
            return View(groupProduct);
        }

        // GET: GroupProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupProduct = await _context.GroupProduct
                .FirstOrDefaultAsync(m => m.GroupProductId == id);
            if (groupProduct == null)
            {
                return NotFound();
            }

            return View(groupProduct);
        }

        // POST: GroupProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupProduct = await _context.GroupProduct.FindAsync(id);
            _context.GroupProduct.Remove(groupProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupProductExists(int id)
        {
            return _context.GroupProduct.Any(e => e.GroupProductId == id);
        }
    }
}
