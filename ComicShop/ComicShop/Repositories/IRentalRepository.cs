using ComicShop.Models;
using ComicShop.Models.DTO;

namespace ComicShop.Repositories;

public interface IRentalRepository
{
    Task<List<Rental>> GetRentalsBetween(DateTime start, DateTime end);
    Task<List<RentalReportDTO>> GetRentalReport(DateTime start, DateTime end);

}