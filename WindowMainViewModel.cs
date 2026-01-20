using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace repairshop_client;

public class WindowMainViewModel : IDisposable
{
	private readonly string hostname;
	private readonly string login;
	private readonly string password;
	private readonly string port;

	private string ConnectionString => $"Host={hostname};Username={login};"
		+ $"Password={password};Database=repairshop;Port={port}";

	private RepairshopContext? dbContext = null;

	public ObservableCollection<Models.Client> Clients { get; private set; } = [];
	public ObservableCollection<Models.Service> Services { get; private set; } = [];
	public ObservableCollection<Models.Mechanic> Mechanics { get; private set; } = [];
	public ObservableCollection<Models.Car> Cars { get; private set; } = [];
	public ObservableCollection<Models.Repair> Repairs { get; private set; } = [];

	private static void ReloadCollection<T> (DbSet<T> dbSet, ObservableCollection<T> collection) where T : class
	{
		//todo: это сомнительное решение
		dbSet.Load();
		collection.Clear();
		foreach (var item in dbSet.Local.ToList()) {
			collection.Add(item);
		}
	}

	private void ReloadCollections ()
	{
		Debug.Assert(this.dbContext != null);

		WindowMainViewModel.ReloadCollection(this.dbContext.Clients, this.Clients);
		WindowMainViewModel.ReloadCollection(this.dbContext.Services, this.Services);
		WindowMainViewModel.ReloadCollection(this.dbContext.Mechanics, this.Mechanics);
		WindowMainViewModel.ReloadCollection(this.dbContext.Repairs, this.Repairs);
		WindowMainViewModel.ReloadCollection(this.dbContext.Cars, this.Cars);
	}

	private bool RefreshConnection ()
	{
		var newDbContext = new RepairshopContext(ConnectionString);
		if (!newDbContext.Database.CanConnect()) {
			newDbContext.Dispose();
			return false;
		}

		this.dbContext?.Dispose();
		this.dbContext = newDbContext;

		ReloadCollections();

		return true;
	}

	private WindowMainViewModel (string hostname, string port, string login, string password)
	{
		this.port = port;
		this.login = login;
		this.password = password;
		this.hostname = hostname;

		if (!this.RefreshConnection()) {
			//todo: свой класс исключений
			throw new Exception("Unable to make new ViewModel");
		}
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

	public int Save () => this.dbContext?.SaveChanges() ?? 0;

	public void DropChanges () => this.RefreshConnection();

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
			this.dbContext?.Dispose();
			this.dbContext = null;
		}
	}
}
