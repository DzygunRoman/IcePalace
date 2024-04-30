using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using AcePalace.Models;
using AcePalace.Areas.Identity.Data;

namespace AcePalace.Controllers
{
    public class ScatesController : Controller
    {
        private readonly AcePalaceContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ScatesController(AcePalaceContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Scates
        public async Task<IActionResult> Index()
        {
            var acePalaceContext = _context.Scates.Include(s => s.Category);
            return View(await acePalaceContext.ToListAsync());
        }

        // GET: Scates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scates = await _context.Scates
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (scates == null)
            {
                return NotFound();
            }

            return View(scates);
        }

        // GET: Scates/Create
        public IActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        // POST: Scates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Size,ProductID,Name,price,Photo,Slug,Description,available,CategoryID")] Scates scates, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    string path = "/img/" + upload.FileName;
                    using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await upload.CopyToAsync(fileStream);
                    }
                    scates.Photo = path;
                }
                _context.Add(scates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDepartmentsDropDownList();
            return View(scates);
        }

        // GET: Scates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scates = await _context.Scates.FindAsync(id);
            if (scates == null)
            {
                return NotFound();
            }
            if (!scates.Photo.IsNullOrEmpty())
            {
                byte[] photodata = System.IO.File.ReadAllBytes(_webHostEnvironment.WebRootPath + scates.Photo);
                ViewBag.Photodata = photodata;
            }
            else
            {
                ViewBag.Photodata = null;
            }
            PopulateDepartmentsDropDownList();
            return View(scates);
        }

        // POST: Scates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Size,ProductID,Name,price,Photo,Slug,Description,available,CategoryID")] Scates scates, IFormFile upload)
        {
            if (id != scates.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (upload != null)
                    {
                        string path = "/img/" + upload.FileName;
                        using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            await upload.CopyToAsync(fileStream);
                        }
                        scates.Photo = path;
                    }
                    _context.Update(scates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScatesExists(scates.ProductID))
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
            PopulateDepartmentsDropDownList();
            return View(scates);
        }
        private void PopulateDepartmentsDropDownList(object selectedCategory = null)
        {
            var categoryQuery = from d in _context.Categories
                                orderby d.categoryName
                                select d;
            ViewBag.CategoryID = new SelectList(categoryQuery.AsNoTracking(), "CategoryID", "categoryName", selectedCategory);
        }
        // GET: Scates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scates = await _context.Scates
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (scates == null)
            {
                return NotFound();
            }

            return View(scates);
        }

        // POST: Scates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scates = await _context.Scates.FindAsync(id);
            if (scates != null)
            {
                _context.Scates.Remove(scates);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScatesExists(int id)
        {
            return _context.Scates.Any(e => e.ProductID == id);
        }
    }
}
