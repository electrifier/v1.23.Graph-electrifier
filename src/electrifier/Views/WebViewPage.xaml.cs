using electrifier.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Windows.System;

namespace electrifier.Views;
// To learn more about WebView2, see https://docs.microsoft.com/microsoft-edge/webview2/.

public sealed partial class WebViewPage : Page
{
    public WebViewViewModel ViewModel
    {
        get;
    }

    public WebViewPage()
    {
        ViewModel = App.GetService<WebViewViewModel>();

        InitializeComponent();

        ViewModel.WebViewService.Initialize(WebViewContent);
    }

    private void AutoSuggestBox_KeyUp(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Enter)
        {
            var urlText = AddressURLAutoSuggestBox.Text;

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