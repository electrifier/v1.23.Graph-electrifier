using electrifier.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace electrifier.Views;

public sealed partial class NetworkDevicesPage : Page
{
    public NetworkDevicesViewModel ViewModel
    {
        get;
    }

    public NetworkDevicesPage()
    {
        ViewModel = App.GetService<NetworkDevicesViewModel>() ?? throw new InvalidOperationException();

        InitializeComponent();
    }
}