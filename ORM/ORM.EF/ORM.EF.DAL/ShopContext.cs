using Microsoft.EntityFrameworkCore;
using ORM.EF.DAL.Models;
using ORM.EF.DAL.Properties;

namespace ORM.EF.DAL;

public class ShopContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Resources.ConnectionString);
    }
}