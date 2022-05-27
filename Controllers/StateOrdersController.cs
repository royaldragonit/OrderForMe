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
    public class StateOrdersController: BaseController
    {
        private readonly OrderformeContext _context;

        public StateOrdersController(OrderformeContext context)
        {
            _context = context;
        }

        // GET: StateOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.StateOrder.ToListAsync());
        }

        // GET: StateOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stateOrder = await _context.StateOrder
                .FirstOrDefaultAsync(m => m.StateOrderId == id);
            if (stateOrder == null)
            {
                return NotFound();
            }

            return View(stateOrder);
        }

        // GET: StateOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StateOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StateOrderId,Name")] StateOrder stateOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stateOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stateOrder);
        }

        // GET: StateOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stateOrder = await _context.StateOrder.FindAsync(id);
            if (stateOrder == null)
            {
                return NotFound();
            }
            return View(stateOrder);
        }

        // POST: StateOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StateOrderId,Name")] StateOrder stateOrder)
        {
            if (id != stateOrder.StateOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stateOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StateOrderExists(stateOrder.StateOrderId))
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
            return View(stateOrder);
        }

        // GET: StateOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stateOrder = await _context.StateOrder
                .FirstOrDefaultAsync(m => m.StateOrderId == id);
            if (stateOrder == null)
            {
                return NotFound();
            }

            return View(stateOrder);
        }

        // POST: StateOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stateOrder = await _context.StateOrder.FindAsync(id);
            _context.StateOrder.Remove(stateOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StateOrderExists(int id)
        {
            return _context.StateOrder.Any(e => e.StateOrderId == id);
        }
    }
}
