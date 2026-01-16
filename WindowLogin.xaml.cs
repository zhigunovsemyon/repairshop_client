using System.Windows;

namespace repairshop_client;

/// <summary>
/// Interaction logic for WindowLogin.xaml
/// </summary>
public partial class WindowLogin : Window
{
	public string Hostname => this.TextBoxHostname.Text;

	public string Login => this.TextBoxLogin.Text;

	public string Password => this.PasswordBox.Password;

	public string Port => this.TextBoxPort.Text;

	public WindowLogin(string hostname, string port, string login, string password)
	{
		InitializeComponent();
		this.TextBoxHostname.Text = hostname;
		this.TextBoxPort.Text = port;
		this.TextBoxLogin.Text = login;
		this.PasswordBox.Password = password;
	}

	private void ButtonOK_Click(object sender, RoutedEventArgs e)
	{
		this.DialogResult = true;
		this.Close();
	}
}
