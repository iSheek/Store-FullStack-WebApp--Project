using System.ComponentModel.DataAnnotations;

namespace Store.API.DTO;

public record CategoryInfoDTO(
    int Id,
    [Required][StringLength(50)] string Name
);