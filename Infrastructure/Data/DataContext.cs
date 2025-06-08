using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<EventEntity> Events { get; set; }
    public DbSet<EventPackageEntity> EventPackages { get; set; }

    public DbSet<PackageEntity> Packages { get; set; }

}
