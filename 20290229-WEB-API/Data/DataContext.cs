

namespace VerifyEmailForgotPasswordTutorial.Data
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
                .UseSqlServer("Server=.\\SQLExpress;Database=FullStackWithFlutter;User Id=sa; Password=P@ssword1;Trusted_Connection=true;TrustServerCertificate=True");
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Store> Stores => Set<Store>();

        public DbSet<Menu>  Menus => Set<Menu>();
    }
}
