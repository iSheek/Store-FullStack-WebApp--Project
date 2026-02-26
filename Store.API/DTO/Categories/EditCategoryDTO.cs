using System.ComponentModel.DataAnnotations;

namespace Store.API.DTO;

public record EditCategoryDTO(
    [Required][StringLength(50)] string Name
);