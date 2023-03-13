using electrifier.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace electrifier.Views;

public sealed partial class ClipboardPage : Page
{
    public ClipboardViewModel ViewModel
    {
        get;
    }

    public ClipboardPage()
    {
        ViewModel = App.GetService<ClipboardViewModel>() ?? throw new InvalidOperationException();

        InitializeComponent();
    }
}