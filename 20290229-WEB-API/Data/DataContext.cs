

using Models.Cart;
using Models.Menu;
using Models.Order;
using Models.OrderDetail;
using Models.Rating;
using Models.Store;
using Models.User;

namespace cemerenbwebapi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer("Server=.\\SQLExpress;Database=cemerenb;User Id=sa; Password=P@ssword1;Trusted_Connection=true;TrustServerCertificate=True");
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Store> Stores => Set<Store>();
        public DbSet<Rating> Ratings => Set<Rating>();
        public DbSet<Menu> Menus => Set<Menu>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
    }
}
