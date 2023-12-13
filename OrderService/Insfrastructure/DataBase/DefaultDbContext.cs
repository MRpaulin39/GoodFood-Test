using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.Insfrastructure.DataBase
{
    public class DefaultDbContext : DbContext
    {
        #region Contructeurs
        public DefaultDbContext(DbContextOptions options) : base(options)
        {

        }

        public DefaultDbContext()
        {
            
        }
        #endregion

        #region Public Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EntityConfiguration.OrderHeaderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfiguration.OrderLineEntityTypeConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=172.17.0.4;Database=OrdersDb;Uid=root;Pwd=password;");
        }
        #endregion

        #region Propriétés
        public DbSet<OrderHeader> OrderHeader { get; set; }
        public DbSet<OrderLine> OrderLine { get; set; }
        #endregion
    }
}
