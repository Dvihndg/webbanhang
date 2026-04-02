using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebBanHangMvc.Models;

namespace WebBanHangMvc.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();

        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        if (!await roleManager.RoleExistsAsync("User"))
            await roleManager.CreateAsync(new IdentityRole("User"));

        const string adminEmail = "admin@webbanhang.local";
        const string adminPassword = "admin123";

        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin is null)
        {
            admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            await userManager.CreateAsync(admin, adminPassword);
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        if (!context.Categories.Any())
        {
            var danhMuc = new[]
            {
                new Category { Name = "Điện thoại" },
                new Category { Name = "Laptop" }
            };
            context.Categories.AddRange(danhMuc);
            await context.SaveChangesAsync();

            context.Products.AddRange(
                new Product { Name = "iPhone 15", Description = "Mẫu điện thoại cao cấp", Price = 22000000, CategoryId = danhMuc[0].Id },
                new Product { Name = "MacBook Air M3", Description = "Laptop mỏng nhẹ", Price = 28900000, CategoryId = danhMuc[1].Id }
            );
            await context.SaveChangesAsync();
        }
    }
}
