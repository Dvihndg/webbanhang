using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHangMvc.Data;
using WebBanHangMvc.Models;

namespace WebBanHangMvc.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public CartController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Challenge();

        var items = await _context.CartItems
            .Where(c => c.UserId == user.Id)
            .Include(c => c.Product)
            .ToListAsync();

        return View(items);
    }

    [HttpPost]
    public async Task<IActionResult> Add(int productId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Challenge();

        var item = await _context.CartItems.FirstOrDefaultAsync(c => c.UserId == user.Id && c.ProductId == productId);
        if (item is null)
        {
            _context.CartItems.Add(new CartItem { UserId = user.Id, ProductId = productId, Quantity = 1 });
        }
        else
        {
            item.Quantity++;
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Challenge();

        var item = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == id && c.UserId == user.Id);
        if (item is not null)
        {
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
