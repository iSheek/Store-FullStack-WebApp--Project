using System;

namespace Store.API.Models;

public class ProductImage
{
    public int Id { get; set; }
    public required string ImagePath { get; set; }
}
