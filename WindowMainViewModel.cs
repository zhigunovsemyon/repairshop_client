using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;

namespace repairshop_client;

public class WindowMainViewModel : IDisposable 
{
	private string hostname = "localhost";
	private string login = "";
	private string password = "";
	private string port = "5432";

	private RepairshopContext? dbContext = null;

	public ObservableCollection<Models.Client> Clients { get; private set; } = [];

	private string connString => $"Host={hostname};Username={login};Password={password};Database=repairshop;Port={port}";

	private bool RefreshDbContext ()
	{
		var newDbContext = new RepairshopContext(this.connString);
		if (!newDbContext.Database.CanConnect()) {
			newDbContext.Dispose();
			return false;
		}

		this.dbContext?.Dispose();
		this.dbContext = newDbContext;

		this.dbContext.Clients.Load();
		this.Clients = this.dbContext.Clients.Local.ToObservableCollection();


		return true;
	}

	public bool Auth ()
	{
		//todo: различать неудачный логин и отмену пользователя
		var windowLogin = new WindowLogin(hostname, port, login, password);
		if (!(windowLogin.ShowDialog() ?? false)) {
			return false;
		}

		this.port = windowLogin.Port;
		this.login = windowLogin.Login;
		this.password = windowLogin.Password;
		this.hostname = String.IsNullOrWhiteSpace(windowLogin.Hostname)
			? "localhost"
			: windowLogin.Hostname;

		return this.RefreshDbContext();
	}

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
