using System.ComponentModel.DataAnnotations;

namespace Store.API.DTO;

public record ProductImageInfoDTO(
    int Id,
    [Required] string ImagePath
);