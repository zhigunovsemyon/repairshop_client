using Microsoft.EntityFrameworkCore;

namespace repairshop_client;

public class RepairshopContext : DbContext
{
	public DbSet<Models.Car> Cars { get; set; }

	public DbSet<Models.Mechanic> Mechanics { get; set; }

	public DbSet<Models.Service> Services { get; set; }

	public DbSet<Models.Client> Clients { get; set; }

	public DbSet<Models.Repair> Repairs { get; set; }

	private readonly string connectionString;

	public RepairshopContext(string connectionString)
	{
		this.connectionString = connectionString;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql(connectionString);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema("repairshop");
	}
}
