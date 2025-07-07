using House.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Windows;

namespace House
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetRequiredService<EquipmentPageViewModel>();
        }

        // 👇 Этот метод должен быть внутри класса, но вне других методов
        private async void Window_Closing(object sender, CancelEventArgs e)
        {
            if (DataContext is EquipmentPageViewModel viewModel)
            {
                try
                {
                    await viewModel.SaveAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}