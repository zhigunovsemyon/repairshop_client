using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace repairshop_client;

public class WindowMainViewModel : IDisposable
{
	private readonly RepairshopContext dbContext; 

	public ObservableCollection<Models.Client> Clients { get; }
	public ObservableCollection<Models.Service> Services { get; }
	public ObservableCollection<Models.Mechanic> Mechanics { get;}
	public ObservableCollection<Models.Car> Cars { get; }
	public ObservableCollection<Models.Repair> Repairs { get; } 

	private WindowMainViewModel (string hostname, string port, string login, string password)
	{
		this.dbContext = new($"Host={hostname};Username={login};"
			+ $"Password={password};Database=repairshop;Port={port}");

		if (!this.dbContext.Database.CanConnect()) {
			this.dbContext.Dispose();
			//todo: свой класс исключений
			throw new Exception("Unable to make new ViewModel");
		}

		this.dbContext.Clients.Load();
		this.dbContext.Cars.Load();
		this.dbContext.Services.Load();
		this.dbContext.Mechanics.Load();
		this.dbContext.Repairs.Load();

		this.Clients = this.dbContext.Clients.Local.ToObservableCollection();
		this.Cars = this.dbContext.Cars.Local.ToObservableCollection();
		this.Mechanics = this.dbContext.Mechanics.Local.ToObservableCollection();
		this.Services = this.dbContext.Services.Local.ToObservableCollection();
		this.Repairs = this.dbContext.Repairs.Local.ToObservableCollection();
	}

	public static WindowMainViewModel? GetNewViewModel (string hostname, string port, string login, string password)
	{
		try {
			return new WindowMainViewModel(
				(String.IsNullOrWhiteSpace(hostname)
					? "localhost"
					: hostname),
				port,
				login,
				password
			);
		}
		catch (Exception) {
			return null;
		}
	}

	public int Save () => this.dbContext.SaveChanges();

	public void Dispose ()
	{
		//CA1816
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose (bool disposing)
	{
		//CA1816
		if (disposing) {
			this.dbContext.Dispose();
		}
	}
}
