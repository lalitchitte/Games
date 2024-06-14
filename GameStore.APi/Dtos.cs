using System.ComponentModel.DataAnnotations;

namespace GameStore.APi.Dtos;

public record GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate,
    string ImageUri
);

public record CreateGameDtos(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(20)] string Genre,
    [Range(1,100)] decimal Price,
    DateOnly ReleaseDate,
    [StringLength(100)][Url] string ImageUri
);

public record UpdateGameDtos(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(20)] string Genre,
    [Range(1,100)] decimal Price,
    DateOnly ReleaseDate,
    [StringLength(100)][Url] string ImageUri
);
