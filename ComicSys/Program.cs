using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =============================
// Add services
// =============================

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var app = builder.Build();

// =============================
// Middleware pipeline
// =============================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// NOTE: Tắt HTTPS redirect để tránh warning local
// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// =============================
// Routing
// =============================

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ComicBooks}/{action=Index}/{id?}"
);

// =============================
// Seed data (ComicBooks)
// =============================

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Chỉ seed khi bảng rỗng
    if (!context.ComicBooks.Any())
    {
        context.ComicBooks.AddRange(
            new ComicBook
            {
                Title = "Conan",
                Author = "Aoyama Gosho",
                PricePerDay = 10
            },
            new ComicBook
            {
                Title = "Doraemon",
                Author = "Fujiko F. Fujio",
                PricePerDay = 8
            }
        );

        context.SaveChanges();
    }
}

app.Run();
