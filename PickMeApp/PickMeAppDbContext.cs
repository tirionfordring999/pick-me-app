using Microsoft.EntityFrameworkCore;
using PickMeApp.Models;
public partial class PickMeAppDbContext : DbContext
{
    public PickMeAppDbContext()
    {
    }

    public PickMeAppDbContext(DbContextOptions<PickMeAppDbContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Users> Users { get; set; }
    public virtual DbSet<Cars> Cars { get; set; }
    public virtual DbSet<Feedbacks> Feedbacks { get; set; }
    public virtual DbSet<Points> Points { get; set; }
    public virtual DbSet<Routes> Routes { get; set; }
    public virtual DbSet<RoutePoints> RoutePoints { get; set; }
    public virtual DbSet<RouteRequest> RouteRequest { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=PickMeApp;Integrated Security=True")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}