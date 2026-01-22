namespace Store.API.DTO;

public record GetProductDTO(
    int Id,
    string Name,
    string Category,
    string ImagePath
);