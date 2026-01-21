using System;

namespace Store.API.Models;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public int ImageId { get; set; }
    public ProductImage? ProductImage { get; set; }

    
}
