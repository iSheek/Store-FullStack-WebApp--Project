using System.ComponentModel.DataAnnotations;

namespace Store.Frontend.DTO;

public record ProductDTO(
    int Id,
    [Required][StringLength(50)] string Name,
    [Required][Range(0.01,10000)] decimal Price,
    int? CategoryId,
    [StringLength(50)]string? Category,
    string? ImagePath
);