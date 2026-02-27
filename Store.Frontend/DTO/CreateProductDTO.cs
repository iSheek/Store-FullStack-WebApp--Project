using System.ComponentModel.DataAnnotations;

namespace Store.Frontend.DTO;

public class CreateProductDTO
{
    [Required][StringLength(50)] public string? Name {get ; set;}
    [Required][Range(0.01,10000)] public decimal? Price {get; set;}
    public int? CategoryId {get; set;}
    [StringLength(50)] public string? Category {get; set;}
    public int? ImageId {get; set;}
}