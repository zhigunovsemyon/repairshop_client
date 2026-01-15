using System.Windows;

namespace repairshop_client;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class WindowMain : Window
{
	private string hostname = "localhost";
	private string login = "";
	private string password = "";
	private string port = "5432";

	private RepairshopContext? dbContext = null;

	private string connString => $"Host={hostname};Username={login};Password={password};Database=repairshop;Port={port}";

	public WindowMain() => this.InitializeComponent();

	private void Window_Loaded(object sender, RoutedEventArgs e)
	{
		this.Auth();
		this.WindowState = WindowState.Normal;
	}

	private bool RefreshDbContext()
	{
		var newDbContext = new RepairshopContext(this.connString);
		if (!newDbContext.Database.CanConnect()) {
			newDbContext.Dispose();
			return false;
		}

		this.dbContext?.Dispose();
		this.dbContext = newDbContext;

		return true;
	}

	private bool Reconnect()
	{
		if (String.IsNullOrWhiteSpace(this.login)) {
			return false;
		}
		if (String.IsNullOrWhiteSpace(this.hostname)) {
			this.hostname = "localhost";
		}

		return RefreshDbContext();
	}

	private bool Auth()
	{
		var windowLogin = new WindowLogin(hostname, port, login, password);
		if (!(windowLogin.ShowDialog() ?? false)) {
			return false;
		}

		this.hostname = windowLogin.Hostname;
		this.port = windowLogin.Port;
		this.login = windowLogin.Login;
		this.password = windowLogin.Password;

		if (this.Reconnect()) {
			this.Tabs.IsEnabled = true;
			return true;
		}
		else {
			MessageBox.Show("Не удалось подключиться с введёнными данными!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			this.Tabs.IsEnabled = false;
			return false;
		}
	}

	private void ButtonAuthClick(object sender, RoutedEventArgs e) => this.Auth();

	private void ButtonCloseClick(object sender, RoutedEventArgs e) => this.Close();

	private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
	{
		this.dbContext?.Dispose();
	}

	private void ButtonSaveClick(object sender, RoutedEventArgs e)
	{
		
	}
}

