using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Windows;

namespace repairshop_client;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class WindowMain : Window
{
	private WindowMainViewModel? ViewModel
		=> this.DataContext as WindowMainViewModel;

	public WindowMain () =>this.InitializeComponent();

	private void Window_Loaded (object sender, RoutedEventArgs e)
	{
		this.Auth();
		this.WindowState = WindowState.Normal;
	}

	private void Auth ()
	{
		var windowLogin = new WindowLogin("localhost", "5432", "", "");
		if (!(windowLogin.ShowDialog() ?? false)) {
			return;
		}

		this.ViewModel?.Dispose();
		this.DataContext = null;
		this.Tabs.IsEnabled = false;

		var newModel = WindowMainViewModel.GetViewModel(windowLogin);
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

	private void ButtonDropClick (object sender, RoutedEventArgs e)
	{
		this.Tabs.IsEnabled = false;
		this.ViewModel?.DropChanges();
		this.Tabs.IsEnabled = true;
	}
}

