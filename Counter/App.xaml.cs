using Microsoft.Maui.Controls;

namespace Counter;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
}
