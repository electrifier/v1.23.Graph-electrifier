using CommunityToolkit.WinUI.UI.Controls;

using electrifier.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace electrifier.Views;

public sealed partial class LocalFilesPage : Page
{
    public LocalFilesViewModel ViewModel
    {
        get;
    }

    public LocalFilesPage()
    {
        ViewModel = App.GetService<LocalFilesViewModel>();
        InitializeComponent();
    }

    private void OnViewStateChanged(object sender, ListDetailsViewState e)
    {
        if (e == ListDetailsViewState.Both)
        {
            ViewModel.EnsureItemSelected();
        }
    }
}
