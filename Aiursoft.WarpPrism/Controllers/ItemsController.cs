using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aiursoft.WarpPrism.Data;
using Aiursoft.WarpPrism.Models;
using Aiursoft.WarpPrism.Models.ItemsViewModels;

namespace Aiursoft.WarpPrism.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index(int id)// Table Id
        {

            var table = await _context
                .Tables
                .Include(t=>t.Properties)
                .Include(t=>t.Items)
                .SingleOrDefaultAsync(t=>t.TableId == id);
            var allValues = await _context
                .Values
                .Include(t => t.ItemContext)
                .Where(t => t.ItemContext.TableId == id)
                .ToListAsync();
            return View( new IndexViewModel
            {
                EntireTable = table,
                AllValues = allValues
            });
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Context)
                .SingleOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public async Task<IActionResult> Create(int id)//Table Id
        {
            var model = new CreateViewModel
            {
                Properties = await _context.Properties.Where(t=>t.TableId == id).ToListAsync(),
                TableId = id
            };
            return View(model);
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Item model)
        {
            var item = new Item
            {
                TableId = model.TableId,
            };
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            foreach(var propertyId in Request.Form.Keys)
            {
                if(int.TryParse(propertyId, out var n))
                {
                    var newValue = new Value
                    {
                        ItemId = item.ItemId,
                        PropertyId = n,
                        RealValue = Request.Form[propertyId]
                    };
                    _context.Values.Add(newValue);
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),new { id = model.TableId});
            //if (ModelState.IsValid)
            //{
            //    _context.Add(item);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["TableId"] = new SelectList(_context.Tables, "TableId", "TableId", item.TableId);
            //return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.SingleOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["TableId"] = new SelectList(_context.Tables, "TableId", "TableId", item.TableId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,TableId")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
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
            ViewData["TableId"] = new SelectList(_context.Tables, "TableId", "TableId", item.TableId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Context)
                .SingleOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.SingleOrDefaultAsync(m => m.ItemId == id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
