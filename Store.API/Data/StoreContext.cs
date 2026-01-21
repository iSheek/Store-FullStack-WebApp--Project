using System;
using Microsoft.EntityFrameworkCore;
using Store.API.Models;


namespace Store.API.Data;

public class StoreContext(DbContextOptions<StoreContext> options)
     : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
}
