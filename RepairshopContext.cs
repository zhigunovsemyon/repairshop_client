using Microsoft.EntityFrameworkCore;

namespace repairshop_client;

public class RepairshopContext (string connectionString) : DbContext
{
	public DbSet<Models.Car> Cars => Set<Models.Car>();

	public DbSet<Models.Mechanic> Mechanics => Set<Models.Mechanic>();

	public DbSet<Models.Service> Services => Set<Models.Service>();

	public DbSet<Models.Client> Clients => Set<Models.Client>();

	public DbSet<Models.Repair> Repairs => Set<Models.Repair>();

	private readonly string connectionString = connectionString;

	protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) 
		=> optionsBuilder
			.UseNpgsql(connectionString)
			.UseLazyLoadingProxies();

	protected override void OnModelCreating (ModelBuilder modelBuilder) 
		=> modelBuilder.HasDefaultSchema("repairshop");
}
