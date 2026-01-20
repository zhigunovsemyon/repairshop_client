using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace repairshop_client;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class WindowMain : Window
{
	private string hostname = "localhost";
	private string port = "5432";
	private string login = "";
	private string password = "";

	private WindowMainViewModel? ViewModel
		=> this.DataContext as WindowMainViewModel;

	public WindowMain () =>this.InitializeComponent();

	private void Window_Loaded (object sender, RoutedEventArgs e)
	{
		this.Auth();
		this.WindowState = WindowState.Normal;
	}

	private bool ShowLoginScreen()
	{
		var windowLogin = new WindowLogin(hostname, port, login, password);
		if (!(windowLogin.ShowDialog() ?? false)) {
			return false;
		}

		this.hostname = windowLogin.Hostname;
		this.login = windowLogin.Login;
		this.password = windowLogin.Password;
		this.port = windowLogin.Port;
		return true;
	}

	private void Auth ()
	{
		if (this.ShowLoginScreen()) {
			this.RefreshConnection();
		}
	}
	
	private void RefreshConnection()
	{
		this.ViewModel?.Dispose();
		this.DataContext = null;
		this.Tabs.IsEnabled = false;

		var newModel = WindowMainViewModel.GetNewViewModel(this.hostname, this.port, this.login, this.password);
		if (newModel is null) {
			MessageBox.Show("Не удалось подключиться с введёнными данными!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		this.DataContext = newModel;
		this.Tabs.IsEnabled = true;
	}

	private void ButtonAuthClick (object sender, RoutedEventArgs e) => this.Auth();

	private void ButtonCloseClick (object sender, RoutedEventArgs e) => this.Close();

	private void Window_Closing (object sender, CancelEventArgs e) => this.ViewModel?.Dispose();

	private void ButtonSaveClick (object sender, RoutedEventArgs e)
	{
		try {
			var changedItems = this.ViewModel?.Save() ?? 0;
			MessageBox.Show($"Изменено {changedItems} записей!", "", MessageBoxButton.OK, MessageBoxImage.Information);
		}
		catch (Exception? ex) {
			string errMsg = "";
			while (ex != null) {
				errMsg += $"\r\n{ex.Message}";
				ex = ex.InnerException;
			}
			MessageBox.Show($"Не удалось изменить записи! Ошибка:{errMsg}", "Ошибка", 
				MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

	private void ButtonDropClick (object sender, RoutedEventArgs e) => this.RefreshConnection();

	private void DataGrid_Loaded (object sender, RoutedEventArgs e)
	{
		var dg = sender as DataGrid;
		Debug.Assert(dg is not null);

		dg.SelectedIndex = dg.Items.Count - 1;
		dg.ScrollIntoView(dg.Items[^1]);
	}
}

