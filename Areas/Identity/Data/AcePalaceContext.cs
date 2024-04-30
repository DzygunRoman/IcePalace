using AcePalace.Areas.Identity.Data;
using AcePalace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AcePalace.Areas.Identity.Data;

public class AcePalaceContext : IdentityDbContext<IdentityUser>
{
    public AcePalaceContext(DbContextOptions<AcePalaceContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Scates> Scates { get; set; }
    public DbSet<Shedule> Shedules { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<CartDetail> CartDetails { get; set; }   
    
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
}
