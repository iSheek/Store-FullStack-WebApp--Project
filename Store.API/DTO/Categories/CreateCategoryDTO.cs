using System.ComponentModel.DataAnnotations;

namespace Store.API.DTO;

public record CreateCategoryDTO(
    [Required][StringLength(50)] string Name
);