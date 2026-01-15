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

	public WindowMain() => this.InitializeComponent();
	private void Window_Loaded(object sender, RoutedEventArgs e)
	{
		if (this.Auth()) { 
			this.WindowState = WindowState.Normal;
		}
		else {
			this.Close();
		}
	}

	private bool Auth ()
	{
		var windowLogin = new WindowLogin(hostname, login, password);
		if (windowLogin.ShowDialog() ?? false) {
			this.Tabs.IsEnabled = true;
			MessageBox.Show($"windowLogin true:{windowLogin.Hostname}\r\n{windowLogin.Login} {windowLogin.Password}");
			return true;
		}
		else {
			MessageBox.Show("windowLogin false");
			this.Tabs.IsEnabled = false;
			return false;
		}
	}

	private void ButtonAuthClick(object sender, RoutedEventArgs e) => this.Auth();

	private void ButtonCloseClick(object sender, RoutedEventArgs e)
	{
		this.Close();
	}

}

