using System.ComponentModel.DataAnnotations;

namespace Store.API.DTO;

public record CategoryDTO(
    int Id,
    [Required][StringLength(50)] string Name
);