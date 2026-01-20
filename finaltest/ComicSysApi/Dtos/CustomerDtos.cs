using System.ComponentModel.DataAnnotations;

namespace ComicSysApi.Dtos;

public record CustomerRegisterDto(
    [Required] string FullName,
    [Required] string PhoneNumber
);
