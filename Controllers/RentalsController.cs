using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Comic_Book_Rental.Data;
using Comic_Book_Rental.Models;
using Comic_Book_Rental.ViewModels; 

namespace Comic_Book_Rental.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ComicSystemContext _context;

        public RentalsController(ComicSystemContext context)
        {
            _context = context;
        }

        // --- YÊU CẦU 3: CHỨC NĂNG THUÊ SÁCH (CUSTOM CODE) ---

        // GET: Rentals/Create
        public IActionResult Create()
        {
            // Load danh sách Khách và Sách vào Dropdown
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName");
            ViewData["ComicBookID"] = new SelectList(_context.ComicBooks, "ComicBookID", "Title");
            return View();
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int CustomerID, int ComicBookID, int Quantity, DateTime ReturnDate)
        {
            // 1. Tạo đơn thuê (Rental)
            var rental = new Rental
            {
                CustomerID = CustomerID,
                RentalDate = DateTime.Now,
                ReturnDate = ReturnDate,
                Status = "Dang thue"
            };

            _context.Add(rental);
            await _context.SaveChangesAsync(); // Lưu để sinh ra RentalID

            // 2. Lấy thông tin sách để lấy giá tiền
            var book = await _context.ComicBooks.FindAsync(ComicBookID);

            // 3. Tạo chi tiết thuê (RentalDetail)
            var rentalDetail = new RentalDetail
            {
                RentalID = rental.RentalID, // Lấy ID vừa tạo ở trên
                ComicBookID = ComicBookID,
                Quantity = Quantity,
                PricePerDay = book.PricePerDay // Lưu giá tại thời điểm thuê
            };

            _context.Add(rentalDetail);
            await _context.SaveChangesAsync();

            // Xong xuôi thì quay về trang báo cáo
            return RedirectToAction(nameof(Report));
        }

        // --- YÊU CẦU 4: BÁO CÁO (REPORT) ---

        // GET: Rentals/Report
        public async Task<IActionResult> Report(DateTime? fromDate, DateTime? toDate)
        {
            // Query kết hợp 4 bảng: Rentals, RentalDetails, ComicBooks, Customers
            var query = from r in _context.Rentals
                        join d in _context.RentalDetails on r.RentalID equals d.RentalID
                        join b in _context.ComicBooks on d.ComicBookID equals b.ComicBookID
                        join c in _context.Customers on r.CustomerID equals c.CustomerID
                        select new RentalReportViewModel // Dùng ViewModel bạn đã tạo
                        {
                            BookName = b.Title,
                            RentalDate = r.RentalDate,
                            ReturnDate = r.ReturnDate,
                            CustomerName = c.FullName,
                            Quantity = d.Quantity
                        };

            // Lọc theo ngày (nếu người dùng chọn)
            if (fromDate.HasValue)
            {
                query = query.Where(x => x.RentalDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                query = query.Where(x => x.RentalDate <= toDate.Value);
            }

            // Sắp xếp giảm dần theo ngày thuê
            return View(await query.OrderByDescending(x => x.RentalDate).ToListAsync());
        }

        // --- CÁC HÀM MẶC ĐỊNH (INDEX, DELETE...) ---

        // GET: Rentals (Danh sách đơn thuê gốc)
        public async Task<IActionResult> Index()
        {
            var comicSystemContext = _context.Rentals.Include(r => r.Customer);
            return View(await comicSystemContext.ToListAsync());
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var rental = await _context.Rentals
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.RentalID == id);

            if (rental == null) return NotFound();

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental != null)
            {
                _context.Rentals.Remove(rental); // Xóa Rental sẽ tự xóa RentalDetail (Cascade Delete)
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}