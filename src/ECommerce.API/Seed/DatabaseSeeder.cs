using ECommerce.DAL.Context;
using ECommerce.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Categories.AnyAsync())
            return;

        var laptops = new Category
        {
            Name = "Laptops",
            Description = "Laptop Computers"
        };

        var mobiles = new Category
        {
            Name = "Mobiles",
            Description = "Smart Phones"
        };

        var accessories = new Category
        {
            Name = "Accessories",
            Description = "Computer Accessories"
        };

        context.Categories.AddRange(
            laptops,
            mobiles,
            accessories
        );

        await context.SaveChangesAsync();

        context.Products.AddRange(

            new Product
            {
                Name = "MacBook Pro M4",
                Description = "Apple Laptop",
                Price = 2200,
                StockQuantity = 15,
                CategoryId = laptops.Id
            },

            new Product
            {
                Name = "Dell XPS 15",
                Description = "Dell Laptop",
                Price = 1800,
                StockQuantity = 12,
                CategoryId = laptops.Id
            },

            new Product
            {
                Name = "iPhone 16 Pro",
                Description = "Apple Phone",
                Price = 1300,
                StockQuantity = 25,
                CategoryId = mobiles.Id
            },

            new Product
            {
                Name = "Samsung Galaxy S25",
                Description = "Samsung Phone",
                Price = 1100,
                StockQuantity = 20,
                CategoryId = mobiles.Id
            },

            new Product
            {
                Name = "Logitech MX Master 3",
                Description = "Wireless Mouse",
                Price = 100,
                StockQuantity = 40,
                CategoryId = accessories.Id
            },

            new Product
            {
                Name = "Mechanical Keyboard",
                Description = "RGB Keyboard",
                Price = 150,
                StockQuantity = 30,
                CategoryId = accessories.Id
            }

        );

        await context.SaveChangesAsync();
    }
}