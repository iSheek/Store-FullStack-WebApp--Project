using System.ComponentModel.DataAnnotations;

namespace Store.API.DTO;

public record CreateProductImageDTO(
    [Required] string ImagePath
);