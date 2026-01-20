using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Đăng ký ApplicationDbContext để sử dụng SQL Server (Thêm đoạn này)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Lưu ý: Nếu .NET phiên bản cũ dùng UseStaticFiles thay vì MapStaticAssets

app.UseRouting();

app.UseAuthorization();

// Nếu dùng .NET 9 mới nhất thì giữ MapStaticAssets, nếu lỗi thì xóa đi dùng app.UseStaticFiles() ở trên
app.MapStaticAssets(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();