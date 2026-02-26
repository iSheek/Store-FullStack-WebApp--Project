using System.ComponentModel.DataAnnotations;

namespace Store.API.DTO;

public record ProductImageDTO(
    int Id,
    [Required] string ImagePath
);