using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace repairshop_client;

public class WindowMainViewModel : IDisposable
{
	private readonly RepairshopContext? dbContext; 

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

	private WindowMainViewModel (string hostname, string port, string login, string password)
	{
		this.dbContext = new($"Host={hostname};Username={login};"
			+ $"Password={password};Database=repairshop;Port={port}");

		if (!this.dbContext.Database.CanConnect()) {
			this.dbContext.Dispose();
			//todo: свой класс исключений
			throw new Exception("Unable to make new ViewModel");
		}
		ReloadCollections();
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
		}
	}
}
