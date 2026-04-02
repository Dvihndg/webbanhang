using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanHangMvc.Data;
using WebBanHangMvc.Models;

namespace WebBanHangMvc.Controllers;

[Authorize(Roles = "Admin")]
public class AdminProductsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.Include(p => p.Category).ToListAsync();
        return View(products);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null) return NotFound();
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Product product)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is not null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
