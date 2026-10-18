using System.Windows;
using CrazyZoo.WpfApp.ViewModels;

namespace CrazyZoo.WpfApp;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        MainWindow window = new()
        {
            DataContext = new MainViewModel()
        };
        window.Show();
    }
}
