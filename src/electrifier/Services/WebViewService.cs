using System.Diagnostics.CodeAnalysis;

using electrifier.Contracts.Services;

using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;

namespace electrifier.Services;

public class WebViewService : IWebViewService
{
    private WebView2? _webView;

    public Uri? Source => _webView?.Source;

    [MemberNotNullWhen(true, nameof(_webView))]
    public bool CanGoBack => _webView != null && _webView.CanGoBack;

    [MemberNotNullWhen(true, nameof(_webView))]
    public bool CanGoForward => _webView != null && _webView.CanGoForward;

    [MemberNotNullWhen(true, nameof(_webView))]
    public string? StatusBarText => _webView?.CoreWebView2.StatusBarText;

    public event EventHandler<CoreWebView2NavigationStartingEventArgs>? NavigationStarting;

    public event EventHandler<CoreWebView2NavigationCompletedEventArgs>? NavigationCompleted;

    public WebViewService()
    {
    }

    [MemberNotNull(nameof(_webView))]
    public void Initialize(WebView2 webView)
    {
        _webView = webView;
        _webView.NavigationStarting += OnWebViewNavigationStarting;
        _webView.NavigationCompleted += OnWebViewNavigationCompleted;

        //_webView.CoreWebView2?.Navigate(@"www.google.de");

        //webView.CoreWebView2.Navigate(String)
        //webView.CoreWebView2.NavigateToString(String);
    }

    public void GoBack() => _webView?.GoBack();

    public void GoForward() => _webView?.GoForward();

    public void Reload() => _webView?.Reload();

//    public void Navigate(string uri) => _webView?.Navigate(uri); ?!?

    public void NavigateToString(string htmlContent) => _webView?.NavigateToString(htmlContent);

    public void UnregisterEvents()
    {
        if (_webView != null)
        {
            _webView.NavigationStarting -= OnWebViewNavigationStarting;
            _webView.NavigationCompleted -= OnWebViewNavigationCompleted;
        }
    }

    private void OnWebViewNavigationStarting(WebView2 sender, CoreWebView2NavigationStartingEventArgs args) => NavigationStarting?.Invoke(this, args);
    private void OnWebViewNavigationCompleted(WebView2 sender, CoreWebView2NavigationCompletedEventArgs args) => NavigationCompleted?.Invoke(this, args);


}