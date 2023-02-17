using Art_Gallery.Models;
using Microsoft.EntityFrameworkCore;

namespace Art_Gallery.DbContextClasses;

public class GallaryDbContext:DbContext
{
    private readonly DbContextOptions<GallaryDbContext> options;

    public GallaryDbContext(DbContextOptions<GallaryDbContext> options):base (options)
    {
        this.options = options;
    }
    public DbSet<Art> arts {get;set;}
    public DbSet<Artist> artists{get; set;}
    public DbSet<Buyers> buyers {get;set;}
    public DbSet<Exhibation> exhibations{get; set;}
    public DbSet<GallaryRepresentative> gallaryRepresentatives {get;set;}
    public DbSet<Gallary> gallaries {get; set;}
    public DbSet<Order> orders{get; set;}
    public DbSet<Cart> carts{get; set;}
    public DbSet<Registration> registrations{get;set;}
    public DbSet<ShippingInfo> shippings{get; set;}
    public DbSet<Admin> admins{get;set;}
    
}