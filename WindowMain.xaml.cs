using System.ComponentModel;
using System.Windows;

namespace repairshop_client;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class WindowMain : Window
{
	private WindowMainViewModel viewModel
		=> (WindowMainViewModel)(this.DataContext);

	public WindowMain ()
	{
		this.InitializeComponent();
		this.DataContext = new WindowMainViewModel();
	}

	private void Window_Loaded (object sender, RoutedEventArgs e)
	{
		this.Auth();
		this.WindowState = WindowState.Normal;
	}

	private bool Auth ()
	{
		//todo: различать неудачный логин и отмену пользователя
		if (this.viewModel.Auth()) {
			this.Tabs.IsEnabled = true;
			return true;
		} else {
			MessageBox.Show("Не удалось подключиться с введёнными данными!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			this.Tabs.IsEnabled = false;
			return false;
		}
	}

	private void ButtonAuthClick (object sender, RoutedEventArgs e) => this.Auth();

	private void ButtonCloseClick (object sender, RoutedEventArgs e) => this.Close();

	private void Window_Closing (object sender, CancelEventArgs e) => this.viewModel.Dispose();

	private void ButtonSaveClick (object sender, RoutedEventArgs e)
	{

	}
}

