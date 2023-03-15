using Azure.Identity;
using CommunityToolkit.Authentication;
using electrifier.ViewModels;
using Microsoft.Graph;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Windows.System;

namespace electrifier.Views;

/// <summary>
/// A page that contains a <seealso cref="WebView2"/>.
///
/// <br/><seealso href="http://aka.ms/winui-project-info">To learn more about WinUI, the WinUI
/// project structure, and more about our project templates</seealso>
/// <br/><seealso href="https://docs.microsoft.com/microsoft-edge/webview2/">Learn more about WebView2.</seealso>
/// </summary>
public sealed partial class WebViewPage : Page
{
    public WebViewViewModel ViewModel
    {
        get;
    }

    public WebViewPage()
    {
        ViewModel = App.GetService<WebViewViewModel>() ?? throw new InvalidOperationException();

        InitializeComponent();

        ViewModel.WebViewService.Initialize(WebViewContent);
    }

    private void AutoSuggestBox_KeyUp(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Enter)
        {
            var urlText = AddressUrlAutoSuggestBox.Text;

            if (!string.IsNullOrWhiteSpace(urlText))
            {
                try
                {
                    ViewModel.BrowseTo(urlText);
                }
                catch (Exception)
                {
                    //ViewModel.HasFailures = true; // Brauchen wir das setzen im ViewModel hier überhaupt?!?
                }
            }
        }
    }
}