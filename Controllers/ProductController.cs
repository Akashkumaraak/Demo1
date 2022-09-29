﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demo.Models;

namespace Demo.Controllers
{
    public class ProductController : Controller
    {
        private readonly BikeContext _context;

        public ProductController(BikeContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var bikeContext = _context.Product1s.Include(p => p.IdNavigation);
            return View(await bikeContext.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product1s == null)
            {
                return NotFound();
            }

            var product1 = await _context.Product1s
                .Include(p => p.IdNavigation)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product1 == null)
            {
                return NotFound();
            }

            return View(product1);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Image,Brand,Price,Stock,CategoryId,Description,Id")] Product1 product1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Categories, "Id", "Id", product1.Id);
            return View(product1);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product1s == null)
            {
                return NotFound();
            }

            var product1 = await _context.Product1s.FindAsync(id);
            if (product1 == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Categories, "Id", "Id", product1.Id);
            return View(product1);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Image,Brand,Price,Stock,CategoryId,Description,Id")] Product1 product1)
        {
            if (id != product1.Id)
            {
                return NotFound();
            }
            else
            {
                _context.Update(product1);
                await _context.SaveChangesAsync();
            }
            ViewData["Id"] = new SelectList(_context.Categories, "Id", "Id", product1.Id);
            return View(product1);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product1s == null)
            {
                return NotFound();
            }

            var product1 = await _context.Product1s
                .Include(p => p.IdNavigation)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product1 == null)
            {
                return NotFound();
            }

            return View(product1);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product1s == null)
            {
                return Problem("Entity set 'BikeContext.Product1s'  is null.");
            }
            var product1 = await _context.Product1s.FindAsync(id);
            if (product1 != null)
            {
                _context.Product1s.Remove(product1);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Product1Exists(int id)
        {
          return _context.Product1s.Any(e => e.ProductId == id);
        }
    }
}
