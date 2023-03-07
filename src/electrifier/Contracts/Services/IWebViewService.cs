using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;

namespace electrifier.Contracts.Services;

/// <summary>
/// See: <see href="https://learn.microsoft.com/en-us/microsoft-edge/webview2/concepts/navigation-events">Navigation events for WebView2 apps</see>
/// </summary>
public interface IWebViewService
{
    Uri? Source
    {
        get;
    }

    bool CanGoBack
    {
        get;
    }

    bool CanGoForward
    {
        get;
    }

    event EventHandler<CoreWebView2NavigationStartingEventArgs>? NavigationStarting;

    event EventHandler<CoreWebView2NavigationCompletedEventArgs>? NavigationCompleted;

    void Initialize(WebView2 webView);

    void GoBack();

    void GoForward();

    void Reload();

    void NavigateToString(string htmlContent);

    void UnregisterEvents();
}