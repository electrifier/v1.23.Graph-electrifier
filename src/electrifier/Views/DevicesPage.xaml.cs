using electrifier.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace electrifier.Views;

public sealed partial class DevicesPage : Page
{
    public DevicesViewModel ViewModel
    {
        get;
    }

    public DevicesPage()
    {
        ViewModel = App.GetService<DevicesViewModel>();
        InitializeComponent();
    }
}