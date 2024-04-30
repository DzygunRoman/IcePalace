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
    public class ProductsController : Controller
    {
        private readonly AcePalaceContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(AcePalaceContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var acePalaceContext = _context.Products.Include(p => p.Category);
            return View(await acePalaceContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (!product.Photo.IsNullOrEmpty())
            {
                byte[] photodata = System.IO.File.ReadAllBytes(_webHostEnvironment.WebRootPath + product.Photo);
                ViewBag.Photodata = photodata;
            }
            else
            {
                ViewBag.Photodata = null;
            }
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,Name,price,Photo,Slug,Description,available,CategoryID,SheduleID")] Product product, IFormFile upload)
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
                    product.Photo = path;
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDepartmentsDropDownList(product.CategoryID);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            if (!product.Photo.IsNullOrEmpty())
            {
                byte[] photodata = System.IO.File.ReadAllBytes(_webHostEnvironment.WebRootPath + product.Photo);
                ViewBag.Photodata = photodata;
            }
            else
            {
                ViewBag.Photodata = null;
            }

            PopulateDepartmentsDropDownList(product.CategoryID);
            
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,Name,price,Photo,Slug,Description,available,CategoryID")] Product product, IFormFile upload)
        {
            if (id != product.ProductID)
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
                        product.Photo = path;
                    }
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
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
            PopulateDepartmentsDropDownList(product.CategoryID);
            return View(product);
        }
        private void PopulateDepartmentsDropDownList(object selectedCategory = null)
        {
            var categoryQuery = from d in _context.Categories
                                orderby d.categoryName
                                select d;
            ViewBag.CategoryID = new SelectList(categoryQuery.AsNoTracking(), "CategoryID", "categoryName", selectedCategory);
        }
        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
