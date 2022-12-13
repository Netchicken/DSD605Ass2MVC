#nullable disable
using DSD605Ass2MVC.Data;
using DSD605Ass2MVC.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DSD605Ass2MVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = _context.Order.Include(o => o.Customer).Include(o => o.Staff).Include(o => o.Stock);
            return View(await orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.Staff)
                .Include(o => o.Stock)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }



            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Name");
            ViewData["StaffId"] = new SelectList(_context.Set<Staff>(), "StaffId", "Name");
            ViewData["StockId"] = new SelectList(_context.Set<Stock>(), "StockId", "ProductName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderDate,ShippedDate,CustomerId,StockId,StaffId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderId = Guid.NewGuid();
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            SetSelectedLists(order);

            //ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Name", order.CustomerId);
            //ViewData["StaffId"] = new SelectList(_context.Set<Staff>(), "StaffId", "Name", order.StaffId);
            //ViewData["StockId"] = new SelectList(_context.Set<Stock>(), "StockId", "ProductName", order.StockId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            SetSelectedLists(order);
            //ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId", order.CustomerId);
            //ViewData["StaffId"] = new SelectList(_context.Set<Staff>(), "StaffId", "Name", order.StaffId);
            //ViewData["StockId"] = new SelectList(_context.Set<Stock>(), "StockId", "ProductName", order.StockId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderId,OrderDate,ShippedDate,CustomerId,StockId,StaffId")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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

            SetSelectedLists(order);

            //ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId", order.CustomerId);
            //ViewData["StaffId"] = new SelectList(_context.Set<Staff>(), "StaffId", "StaffId", order.StaffId);
            //ViewData["StockId"] = new SelectList(_context.Set<Stock>(), "StockId", "StockId", order.StockId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.Staff)
                .Include(o => o.Stock)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }

        /// <summary>
        /// Making the ViewData into one method and calling it
        /// </summary>
        /// <param name="order"></param>
        private void SetSelectedLists(Order order)
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Name", order.CustomerId);
            ViewData["StaffId"] = new SelectList(_context.Set<Staff>(), "StaffId", "Name", order.StaffId);
            ViewData["StockId"] = new SelectList(_context.Set<Stock>(), "StockId", "ProductName", order.StockId);


        }

    }
}
