using electrifier.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace electrifier.Views;

public sealed partial class WebFavoritesPage : Page
{
    public WebFavoritesViewModel ViewModel
    {
        get;
    }

    public WebFavoritesPage()
    {
        ViewModel = App.GetService<WebFavoritesViewModel>();
        InitializeComponent();
    }
}