using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;

namespace ComicSystem.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // ==========================================
        // CÂU 2: ĐĂNG KÝ KHÁCH HÀNG (3 ĐIỂM)
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,PhoneNumber")] Customer customer)
        {
            // Tự động gán ngày đăng ký là thời điểm hiện tại
            customer.RegistrationDate = DateTime.Now; 

            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // ==========================================
        // CÂU 4: BÁO CÁO LƯỢT THUÊ (3 ĐIỂM)
        // ==========================================
        public async Task<IActionResult> RentalReport(DateTime? startDate, DateTime? endDate)
        {
            // Truy vấn kết hợp các bảng theo sơ đồ quan hệ
            var query = from rd in _context.RentalDetails
                        join r in _context.Rentals on rd.RentalID equals r.RentalID
                        join c in _context.Customers on r.CustomerID equals c.CustomerID
                        join b in _context.ComicBooks on rd.ComicBookID equals b.ComicBookID
                        select new 
                        {
                            BookName = b.Title,
                            RentalDate = r.RentalDate,
                            ReturnDate = r.ReturnDate,
                            CustomerName = c.FullName,
                            Quantity = rd.Quantity
                        };

            // Lọc theo khoảng thời gian nếu người dùng nhập ngày
            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(x => x.RentalDate >= startDate && x.RentalDate <= endDate);
            }

            // Gửi dữ liệu ra View
            var result = await query.ToListAsync();
            return View(result);
        }

        // Các hàm Edit, Delete, Details bạn có thể giữ nguyên như cũ 
        // hoặc chạy lại lệnh Scaffolding nếu cần.
    }
}