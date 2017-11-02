using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aiursoft.WarpPrism.Data;
using Aiursoft.WarpPrism.Models;

namespace Aiursoft.WarpPrism.Controllers
{
    public class DataBasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DataBasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DataBases
        public async Task<IActionResult> Index()
        {
            return View(await _context.Databases.ToListAsync());
        }

        // GET: DataBases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataBase = await _context.Databases
                .SingleOrDefaultAsync(m => m.DataBaseId == id);
            if (dataBase == null)
            {
                return NotFound();
            }

            return View(dataBase);
        }

        // GET: DataBases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DataBases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataBaseId,DataBaseName,CreateTime")] DataBase dataBase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataBase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dataBase);
        }

        // GET: DataBases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataBase = await _context.Databases.SingleOrDefaultAsync(m => m.DataBaseId == id);
            if (dataBase == null)
            {
                return NotFound();
            }
            return View(dataBase);
        }

        // POST: DataBases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DataBaseId,DataBaseName,CreateTime")] DataBase dataBase)
        {
            if (id != dataBase.DataBaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataBase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataBaseExists(dataBase.DataBaseId))
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
            return View(dataBase);
        }

        // GET: DataBases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataBase = await _context.Databases
                .SingleOrDefaultAsync(m => m.DataBaseId == id);
            if (dataBase == null)
            {
                return NotFound();
            }

            return View(dataBase);
        }

        // POST: DataBases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataBase = await _context.Databases.SingleOrDefaultAsync(m => m.DataBaseId == id);
            _context.Databases.Remove(dataBase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DataBaseExists(int id)
        {
            return _context.Databases.Any(e => e.DataBaseId == id);
        }
    }
}
