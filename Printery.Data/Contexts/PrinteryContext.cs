using Printery.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Contexts
{
    public interface IPrinteryContext
    {
        DbSet<Order> Order { get; set; }
        DbSet<InkStock> InkStock { get; set; }
        DbSet<InkPurchasing> InkPurchasing { get; set; }
        DbSet<OrderDetail> OrderDetail { get; set; }
        DbSet<Paper> Paper { get; set; }
        DbSet<PaperPurchasing> PaperPurchasing { get; set; }
        DbSet<Product> Product { get; set; }
        DbSet<ProductGoods> ProductGoods { get; set; }
        Database GetDb();
        DbContext GetDbContext();
    }
    public class PrinteryContext:DbContext,IPrinteryContext
    {
        public PrinteryContext() : base("name=Printery")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<PrinteryContext>());
            Database.SetInitializer<PrinteryContext>(null);
        }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<InkStock> InkStock { get; set; }
        public virtual DbSet<InkPurchasing> InkPurchasing { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Paper> Paper { get; set; }
        public virtual DbSet<PaperPurchasing> PaperPurchasing { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductGoods> ProductGoods { get; set; }
        public Database GetDb()
        {
            return Database;
        }
        public DbContext GetDbContext()
        {
            return this;
        }
    }
}
