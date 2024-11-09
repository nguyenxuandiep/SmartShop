using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web_Shop.Models;

namespace Web_Shop.Repository
{
    public class DataContext : IdentityDbContext<AppUseModel>
    {
        

        public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}
		public DbSet<BrandModel> Brands { get; set; }
		public DbSet<ProductModel> Products { get; set; }
		public DbSet<CategoryModel> Categories { get; set; }
		public DbSet<OrderModel>	Orders { get; set; }
		public DbSet<OrderDetails> OrderDetails { get; set; }
	}

    
}
