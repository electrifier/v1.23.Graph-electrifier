using Azure.Identity;
using CommunityToolkit.Authentication;
using electrifier.ViewModels;
using Microsoft.Graph;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

namespace electrifier.Views;

/// <summary>
/// A page that directly loads Microsoft 365 start page using <seealso cref="WebView2"/>.
/// Can be used on its own or navigated to within a Frame
///
/// <seealso href="http://aka.ms/winui-project-info">To learn more about WinUI, the WinUI
/// project structure, and more about our project templates</seealso>
/// </summary>
public sealed partial class Microsoft365Page : Page
{
    public Microsoft365ViewModel ViewModel
    {
        get;
    }

    public Microsoft365Page()
    {
        ViewModel = App.GetService<Microsoft365ViewModel>() ?? throw new InvalidOperationException();

        this.InitializeComponent();

//        WebView.Source = new Uri("Source=\"https://www.office.com/?acctsw=1&auth=1\"");
    }
}