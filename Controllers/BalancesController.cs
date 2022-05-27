using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderForMeProject.Commons;
using OrderForMeProject.Models.CustomModels;
using OrderForMeProject.Models.Entities;
using OrderForMeProject.Services;

namespace OrderForMeProject.Controllers
{
    public class BalancesController: BaseController
    {
        private readonly OrderformeContext _context;
        private readonly IBalancesServices _balancesServices;

        public BalancesController(OrderformeContext context, IBalancesServices balancesServices)
        {
            _context = context;
            _balancesServices = balancesServices;
        }

        // GET: Balances
        public async Task<IActionResult> Index()
        {
            var data = _balancesServices.Include();
            return View(data);
        }


        // GET: Balances
        public async Task<IActionResult> Accept(int LoggerId)
        {
           await _balancesServices.Accept(LoggerId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ResultCustomModel<bool>> ChargeMoney(float money)
        {
            if (User.Identity.IsAuthenticated)
            {
                _balancesServices.ChargeMoney(User.GetUserId(),money);
                return new ResultCustomModel<bool>
                {
                    Data = true,
                    Success = true,
                    Message = "Đơn nạp tiền của bạn đã được gửi"
                };
            }
            else
            {
                return new ResultCustomModel<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Bạn cần đăng nhập để có thể nạp tiền"
                };
            }
        }
        // GET: Balances
        public async Task<IActionResult> Reject(int LoggerId)
        {
            var logger = _context.Logger.Find(LoggerId);
            logger.IsAccept = false;
            _context.Update(logger);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Balances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logger = await _context.Logger
                .Include(l => l.Users)
                .FirstOrDefaultAsync(m => m.LoggerId == id);
            if (logger == null)
            {
                return NotFound();
            }

            return View(logger);
        }

        // GET: Balances/Create
        public IActionResult Create()
        {
            ViewData["UsersId"] = new SelectList(_context.Users, "UsersId", "Fullname");
            return View();
        }

        // POST: Balances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoggerId,UsersId,Balance,Type,IsAccept,CreateOn")] Logger logger)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logger);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsersId"] = new SelectList(_context.Users, "UsersId", "Fullname", logger.UsersId);
            return View(logger);
        }

        // GET: Balances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logger = await _context.Logger.FindAsync(id);
            if (logger == null)
            {
                return NotFound();
            }
            ViewData["UsersId"] = new SelectList(_context.Users, "UsersId", "Fullname", logger.UsersId);
            return View(logger);
        }

        // POST: Balances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoggerId,UsersId,Balance,Type,IsAccept,CreateOn")] Logger logger)
        {
            if (id != logger.LoggerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoggerExists(logger.LoggerId))
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
            ViewData["UsersId"] = new SelectList(_context.Users, "UsersId", "Fullname", logger.UsersId);
            return View(logger);
        }

        // GET: Balances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logger = await _context.Logger
                .Include(l => l.Users)
                .FirstOrDefaultAsync(m => m.LoggerId == id);
            if (logger == null)
            {
                return NotFound();
            }

            return View(logger);
        }

        // POST: Balances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logger = await _context.Logger.FindAsync(id);
            _context.Logger.Remove(logger);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoggerExists(int id)
        {
            return _context.Logger.Any(e => e.LoggerId == id);
        }
    }
}
