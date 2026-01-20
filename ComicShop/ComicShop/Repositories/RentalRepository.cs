using ComicShop.Controllers;
using ComicShop.Models;
using ComicShop.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace ComicShop.Repositories;

public class RentalRepository : IRentalRepository
{
    private readonly AppDbContext _context;

    public RentalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Rental>> GetRentalsBetween(DateTime start, DateTime end)
    {
        return await _context.rentals
            .Include(r => r.customer)             
            .Include(r => r.rentaldetail)        
            .ThenInclude(rd => rd.comicbook)   
            .Where(r => r.rentaldate >= start && r.rentaldate <= end)
            .ToListAsync();
    }
    public async Task<List<RentalReportDTO>> GetRentalReport(DateTime start, DateTime end)
    {
        var data = await _context.rentaldetails
            .Include(rd => rd.rental)
            .ThenInclude(r => r.customer)
            .Include(rd => rd.comicbook)
            .Where(rd =>
                rd.rental.rentaldate >= start &&
                rd.rental.rentaldate <= end)
            .Select(rd => new RentalReportDTO
            {
                bookname = rd.comicbook.title,
                rentaldate = rd.rental.rentaldate,
                returndate = rd.rental.returndate,
                customername = rd.rental.customer.fullname,
                quantity = rd.quantity
            })
            .ToListAsync();
                int no = 1;
        data.ForEach(d => d.no = no++);

        return data;
    }
}