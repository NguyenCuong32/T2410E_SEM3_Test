using System.ComponentModel.DataAnnotations;

namespace ComicSysApi.Dtos;

public record ComicBookCreateDto(
    [Required] string Title,
    [Required] string Author,
    [Range(0, 99999999)] decimal PricePerDay
);

public record ComicBookUpdateDto(
    [Required] string Title,
    [Required] string Author,
    [Range(0, 99999999)] decimal PricePerDay
);
