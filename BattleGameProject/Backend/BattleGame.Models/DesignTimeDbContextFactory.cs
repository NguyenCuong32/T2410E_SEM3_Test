using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BattleGame.Data;  // Import DbContext namespace

namespace BattleGame.Models  // Hoặc dùng BattleGame.Data nếu bạn muốn
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BattleGameDbContext>
    {
        public BattleGameDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BattleGameDbContext>();

            // Thay bằng conn string thực tế từ Azure SQL (BATTLEGAME database)
            // Ví dụ: Server=tcp:yourserver.database.windows.net,1433;Initial Catalog=BATTLEGAME;User ID=youruser;Password=yourpass;Encrypt=True;
            var connectionString = "Server=tcp:yourserver.database.windows.net,1433;Initial Catalog=BATTLEGAME;Persist Security Info=False;User ID=youruser;Password=yourpass;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            optionsBuilder.UseSqlServer(connectionString);

            return new BattleGameDbContext(optionsBuilder.Options);
        }
    }
}