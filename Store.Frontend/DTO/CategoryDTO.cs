using System.ComponentModel.DataAnnotations;

namespace Store.Frontend.DTO;

public record CategoryDTO(
    int Id,
    [Required][StringLength(50)] string Name
);