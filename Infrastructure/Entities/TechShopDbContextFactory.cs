using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Entities
{
	//TNT91
	public class TechShopDbContextFactory : IDesignTimeDbContextFactory<TechShopDbContext>
    {
        public TechShopDbContext CreateDbContext(string[] args)
        {
            // chỉ ra sẽ đọc dữ liệu ở trong file appsettings.json của Infrastructure
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<TechShopDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new TechShopDbContext(optionsBuilder.Options);
        }
    }
}
