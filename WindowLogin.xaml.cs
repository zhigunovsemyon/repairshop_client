using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

	public WindowLogin(string hostname, string login, string password)
	{
		InitializeComponent();
		this.TextBoxHostname.Text = hostname;
		this.TextBoxLogin.Text = login;
		this.PasswordBox.Password = password;
	}

	private void ButtonOK_Click(object sender, RoutedEventArgs e)
	{
		this.DialogResult = true;
		this.Close();
	}
}
