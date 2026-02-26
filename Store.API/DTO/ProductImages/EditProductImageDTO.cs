using System.ComponentModel.DataAnnotations;

namespace Store.API.DTO;

public record EditProductImageDTO(
    [Required] string ImagePath
);