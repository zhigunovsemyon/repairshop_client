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

	private bool VerifyConnectionData()
	{
		if (String.IsNullOrWhiteSpace(this.login)) {
			return false;
		}
		if (String.IsNullOrWhiteSpace(this.hostname)) {
			this.hostname = "localhost";
		}
		return true;
	}

	private bool Auth()
	{
		var windowLogin = new WindowLogin(hostname, login, password);
		if (!(windowLogin.ShowDialog() ?? false)) {
			return false;
		}

		this.hostname = windowLogin.Hostname;
		this.login = windowLogin.Login;
		this.password = windowLogin.Password;

		if (this.VerifyConnectionData()) {
			this.Tabs.IsEnabled = true;
			MessageBox.Show($"windowLogin true:{this.hostname}\r\n{this.login} {this.password}");
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

