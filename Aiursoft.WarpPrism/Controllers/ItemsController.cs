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
                .Include(t => t.Properties)
                .Include(t => t.Items)
                .SingleOrDefaultAsync(t => t.TableId == id);
            var allValues = await _context
                .Values
                .Include(t => t.ItemContext)
                .Where(t => t.ItemContext.TableId == id)
                .ToListAsync();
            return View(new IndexViewModel
            {
                EntireTable = table,
                AllValues = allValues
            });
        }

        // GET: Items/Create
        public async Task<IActionResult> Create(int id)//Table Id
        {
            var model = new CreateViewModel
            {
                Properties = await _context.Properties.Where(t => t.TableId == id).ToListAsync(),
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
            foreach (var propertyId in Request.Form.Keys)
            {
                if (int.TryParse(propertyId, out var n))
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
            return RedirectToAction(nameof(Index), new { id = model.TableId });
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Items.SingleOrDefaultAsync(t=>t.ItemId == id);
            var model = new EditViewModel
            {
                Properties = await _context.Properties.Where(t => t.TableId == item.TableId).ToListAsync(),
                TableId = item.TableId,
                TargetItemId = id
            };
            return View(model);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            _context.Values.RemoveRange(_context.Values.Where(t => t.ItemId == model.TargetItemId));
            await _context.SaveChangesAsync();
            var item = await _context.Items.SingleOrDefaultAsync(t=>t.ItemId == model.TargetItemId);
            foreach (var propertyId in Request.Form.Keys)
            {
                if (int.TryParse(propertyId, out var n))
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
            return RedirectToAction(nameof(Index), new { id = model.TableId });
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
            _context.Values.RemoveRange(_context.Values.Where(t=>t.ItemId == id));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = item.TableId });
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
