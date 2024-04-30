using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcePalace.Models;
using AcePalace.Areas.Identity.Data;

namespace AcePalace.Controllers
{
    public class ShedulesController : Controller
    {
        private readonly AcePalaceContext _context;

        public ShedulesController(AcePalaceContext context)
        {
            _context = context;
        }

        // GET: Shedules
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shedules.OrderBy(s =>s.DateTime).ToListAsync());
        }

        // GET: Shedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shedule = await _context.Shedules
                .FirstOrDefaultAsync(m => m.SheduleID == id);
            if (shedule == null)
            {
                return NotFound();
            }

            return View(shedule);
        }

        // GET: Shedules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SheduleID,SheduleName,Trener,DateTime")] Shedule shedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shedule);
        }

        // GET: Shedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shedule = await _context.Shedules.FindAsync(id);
            if (shedule == null)
            {
                return NotFound();
            }
            return View(shedule);
        }

        // POST: Shedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SheduleID,SheduleName,Trener,DateTime")] Shedule shedule)
        {
            if (id != shedule.SheduleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SheduleExists(shedule.SheduleID))
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
            return View(shedule);
        }

        // GET: Shedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shedule = await _context.Shedules
                .FirstOrDefaultAsync(m => m.SheduleID == id);
            if (shedule == null)
            {
                return NotFound();
            }

            return View(shedule);
        }

        // POST: Shedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shedule = await _context.Shedules.FindAsync(id);
            if (shedule != null)
            {
                _context.Shedules.Remove(shedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SheduleExists(int id)
        {
            return _context.Shedules.Any(e => e.SheduleID == id);
        }
    }
}
