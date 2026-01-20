namespace ComicSysApi.Dtos;

public record RentalReportRowDto(
    string BookName,
    DateTime RentalDate,
    DateTime ReturnDate,
    string CustomerName,
    int Quantity
);
