using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderForMeProject.Commons;
using OrderForMeProject.Models.Entities;

namespace OrderForMeProject.Controllers
{
    [Authorize]
    public class UsersController: Controller
    {
        private readonly OrderformeContext _context;

        public UsersController(OrderformeContext context)
        {
            _context = context;
        }

        [Authorize(Roles = AuthConst.Administrator)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UsersId == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        [Authorize(Roles = AuthConst.Administrator)]
        public async Task<IActionResult> Create()
        {
            var roles = await _context.Role.ToListAsync();
            ViewBag.Role = roles;
            return View();
        }

        [Authorize(Roles = AuthConst.Administrator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsersId,Username,Password,Fullname,Address,Phone,Email,Balance,CreateOn,IsActive")] Users users)
        {
            var roles = await _context.Role.ToListAsync();
            ViewBag.Role = roles;
            users.Balance = 0;
            users.Password = users.Password.ToMD5();
            users.IsActive = true;
            users.CreateOn = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var roles = await _context.Role.ToListAsync();
            ViewBag.Role = roles;
            if (id == null)
            {
                return NotFound();
            }
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            string role = User.GetUserRole();
            if (role==AuthConst.Administrator)
            {

            }
            else
            {
                if (User.GetUserId() != users.UsersId)
                {
                    //Nếu user đang cố tình sửa user của người khác, ví dụ user có userid=3 mà gõ /Users/Edit/4
                    return BadRequest();
                }
            }
            return View(users);
        }
        // GET: Users/Edit/5
        public async Task<IActionResult> TransactionHistory(int? id)
        {           
            if (id == null)
            {
                return NotFound();
            }
            string role = User.GetUserRole();
            if (role==AuthConst.Administrator)
            {

            }
            else
            {
                if (User.GetUserId() != id)
                {
                    //Nếu user đang cố tình sửa user của người khác, ví dụ user có userid=3 mà gõ /Users/Edit/4
                    return BadRequest();
                }
            }
            List<Logger> log = await _context.Logger.Where(x => x.UsersId == id).ToListAsync();
            return View(log);
        }
        [HttpGet]
        public async Task<IActionResult> CreateTransaction(int balance)
        {
            int id = User.GetUserId();
            Logger log = new Logger()
            {
                Balance=balance,
                UsersId= id,
                CreateOn=DateTime.Now,
                IsAccept=false,
                Type="1"
            };
            _context.Add(log);
            await _context.SaveChangesAsync();
            List<Logger> logger = await _context.Logger.Where(x => x.UsersId == id).ToListAsync();
            return View("~/Views/Users/TransactionHistory.cshtml", logger);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsersId,Username,Fullname,Address,Phone,Email,CreateOn,IsActive")] Users users)
        {
            if (id != users.UsersId)
            {
                return NotFound();
            }
            string role = User.GetUserRole();
            if (role == AuthConst.Administrator)
            {

            }
            else
            {
                if (User.GetUserId() != users.UsersId)
                {
                    //Nếu user đang cố tình sửa user của người khác, ví dụ user có userid=3 mà gõ /Users/Edit/4
                    return BadRequest();
                }
            }
            Users userUpdate = await _context.Users.FindAsync(id);
            userUpdate.Fullname = users.Fullname;
            userUpdate.Email = users.Email;
            userUpdate.Phone = users.Phone;
            userUpdate.Address = users.Address;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.UsersId))
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
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UsersId == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UsersId == id);
        }
    }
}
