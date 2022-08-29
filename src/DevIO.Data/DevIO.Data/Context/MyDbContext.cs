using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Context;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Provider> Providers { get; set; }
}
