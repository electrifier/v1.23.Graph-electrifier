using electrifier.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace electrifier.Views;

public sealed partial class WebHostsPage : Page
{
    public WebHostsViewModel ViewModel
    {
        get;
    }

    public WebHostsPage()
    {
        ViewModel = App.GetService<WebHostsViewModel>();
        InitializeComponent();
    }

    public bool ApacheLicenseInfoBarVisibility => true; //ApacheLicenseInfoBar.IsVisible;
}