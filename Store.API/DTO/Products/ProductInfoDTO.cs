using System.ComponentModel.DataAnnotations;

namespace Store.API.DTO;

public record ProductInfoDTO(
    int Id,
    [Required][StringLength(50)] string Name,
    [Required][Range(0.01,10000)] decimal Price,
    int? CategoryId,
    int? ImagePathId
);