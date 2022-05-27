using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderForMeProject.Commons;
using OrderForMeProject.Models.CustomModels;
using OrderForMeProject.Models.Entities;
using OrderForMeProject.Models.OrdersModels;
using OrderForMeProject.Services;

namespace OrderForMeProject.Controllers
{
    [Authorize]
    public class OrdersController: Controller
    {
        private readonly OrderformeContext _db;
        private readonly IOrderServices _orderServices;

        public OrdersController(OrderformeContext db, IOrderServices orderServices)
        {
            _db = db;
            _orderServices = orderServices;
        }

        // GET: Orders
        public async Task<IActionResult> Index(int filter)
        {
            IQueryable<VListOrder> orderformeContext;
            //Lấy tất cả
            if (filter <= 0)
                orderformeContext = _db.VListOrder;
            else
                orderformeContext = _db.VListOrder.Where(x => x.StateOrderId == filter);
            ViewBag.StateOrder = await _db.StateOrder.ToListAsync();
            return View(await orderformeContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _db.Orders
                .Include(o => o.GroupProduct)
                .Include(o => o.SiteBuy)
                .Include(o => o.SourceBuy)
                .Include(o => o.StateOrder)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.OrdersId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            var bindingOrder = await _orderServices.GetInfoCreateOrder();
            return View(bindingOrder.Data);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrdersModel orders)
        {
            if (ModelState.IsValid)
            {
                await _orderServices.CreateOrder(orders);
                return RedirectToAction(nameof(Index));
            }
            var bindingOrder = await _orderServices.GetInfoCreateOrder();
            return View(bindingOrder.Data);
        }
        [HttpGet]
        public async Task<Users> GetInfoCustomer(int usersId)
        {
            var users = await _orderServices.GetInfoCustomer(usersId);
            return users;
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _db.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            var bindingOrder = await _orderServices.GetInfoCreateOrder();
            bindingOrder.Data.Order = orders;
            return View(bindingOrder.Data);
        }
        // GET: Orders/Split/5
        public async Task<IActionResult> Split(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _db.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            var bindingOrder = await _orderServices.GetInfoCreateOrder();
            bindingOrder.Data.Order = orders;
            return View(bindingOrder.Data);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ResultCustomModel<bool>> Split(int id, [FromBody] List<SplitOrdersModel> orders)
        {            
          var data=  await _orderServices.SplitOrder(id, orders);
            return data;
        }
        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateOrdersModel orders)
        {
            if (id != orders.OrdersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _orderServices.UpdateOrder(orders);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrdersId))
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
            ViewData["GroupProductId"] = new SelectList(_db.GroupProduct, "GroupProductId", "Name", orders.GroupProductId);
            ViewData["SiteBuyId"] = new SelectList(_db.SiteBuy, "SiteBuyId", "Name", orders.SiteBuyId);
            ViewData["SourceBuyId"] = new SelectList(_db.SourceBuy, "SourceBuyId", "Name", orders.SourceBuyId);
            ViewData["StateOrderId"] = new SelectList(_db.StateOrder, "StateOrderId", "StateOrderId", orders.StateOrderId);
            ViewData["UsersId"] = new SelectList(_db.Users, "UsersId", "Fullname", orders.UsersId);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _db.Orders
                .Include(o => o.GroupProduct)
                .Include(o => o.SiteBuy)
                .Include(o => o.SourceBuy)
                .Include(o => o.StateOrder)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.OrdersId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _db.Orders.FindAsync(id);
            _db.Orders.Remove(orders);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _db.Orders.Any(e => e.OrdersId == id);
        }
    }
}
