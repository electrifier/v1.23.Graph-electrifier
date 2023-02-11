using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using electrifier.Contracts.Services;
using electrifier.Contracts.ViewModels;

using Microsoft.Web.WebView2.Core;

namespace electrifier.ViewModels;
// TODO: Review best practices and distribution guidelines for WebView2.

/// <summary>
/// To <see href="https://docs.microsoft.com/microsoft-edge/webview2/get-started/winui">get started using WebView2</see>
/// , see <see href="https://docs.microsoft.com/microsoft-edge/webview2/concepts/developer-guide">Developer guide</see>
/// and <see href="https://docs.microsoft.com/microsoft-edge/webview2/concepts/distribution">Distribution</see>.
/// </summary>
public class WebViewViewModel : ObservableRecipient, INavigationAware
{
    /// <summary>Default URI to display</summary>
    private const string _defaultSourceOnLaunch = @"https://docs.microsoft.com/windows/apps/";

    /// <summary>Backing fields</summary>
    private string _currentSourceForDisplay = _defaultSourceOnLaunch;
    private Uri _source = new(_defaultSourceOnLaunch);
    private bool _isLoading = true;
    private bool _hasFailures;

    public IWebViewService WebViewService
    {
        get;
    }

    public string CurrentSourceForDisplay
    {
        get => _currentSourceForDisplay;
        private set => SetProperty(ref _currentSourceForDisplay, value);
    }

    public Uri Source
    {
        get => _source;
        private set => SetProperty(ref _source, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public bool HasFailures
    {
        get => _hasFailures;
        set => SetProperty(ref _hasFailures, value);
    }

    public ICommand BrowserBackCommand
    {
        get;
    }

    public ICommand BrowserForwardCommand
    {
        get;
    }

    public ICommand ReloadCommand
    {
        get;
    }

    public ICommand OpenInBrowserCommand
    {
        get;
    }

    public WebViewViewModel(IWebViewService webViewService)
    {
        WebViewService = webViewService;

        BrowserBackCommand = new RelayCommand(() => WebViewService.GoBack(), () => WebViewService.CanGoBack);
        BrowserForwardCommand = new RelayCommand(() => WebViewService.GoForward(), () => WebViewService.CanGoForward);
        ReloadCommand = new RelayCommand(OnReloadCommand);
        OpenInBrowserCommand = new RelayCommand(async () => await Windows.System.Launcher.LaunchUriAsync(WebViewService.Source), () => WebViewService.Source != null);
    }

    public void OnNavigatedTo(object parameter)
    {
        WebViewService.NavigationStarting += OnWebViewNavigationStarting;
        WebViewService.NavigationCompleted += OnWebViewNavigationCompleted;
    }

    public void OnNavigatedFrom()
    {
        WebViewService.UnregisterEvents();
        WebViewService.NavigationStarting -= OnWebViewNavigationStarting;
        WebViewService.NavigationCompleted -= OnWebViewNavigationCompleted;
    }

    private void OnWebViewNavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs naviStarting)
    {
        IsLoading = true;
        CurrentSourceForDisplay = naviStarting.Uri;

        if (naviStarting.IsUserInitiated)
        {
        }

        if (naviStarting.IsRedirected)
        {
        }
    }

    private void OnWebViewNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs naviCompleted)
    {
        IsLoading = false;

        OnPropertyChanged(nameof(BrowserBackCommand));
        OnPropertyChanged(nameof(BrowserForwardCommand));
        OnPropertyChanged(nameof(ReloadCommand));

        if (naviCompleted.IsSuccess)
        {
            HasFailures = false;
        }
        else
        {
            var webErrorStatus = naviCompleted.WebErrorStatus;



            //if (naviStarting.WebErrorStatus == CoreWebView2WebErrorStatus.HostNameNotResolved) { }


            //var errorHtmlSource = $"<h1>Die gewünschte Seite konnte nicht geladen werden.</h1><br/><h2>Fehlerursache: <i>{naviCompleted.WebErrorStatus}</i></h2></html>";
            //WebViewService.NavigateToString(errorHtmlSource);

            HasFailures = true;

            // TODO: neue URL setzen?!? um endlos-Schleife zu verhindern
        }
    }



    private void OnReloadCommand()
    {
        HasFailures = false;
        IsLoading = true;

        WebViewService.Reload();
    }

    private void OnTestCase()
    {
        // TODO: TestCases
        // TODO: (!) "www.klackedieklack", "https://www.klickklackedie/"?
    }


    internal void BrowseTo(string stringEncodedUri)
    {
        HasFailures = false;
        IsLoading = true;

        if (stringEncodedUri.Where(x => (x == '.')).Count() < 2)
        {
            stringEncodedUri = $"www.{stringEncodedUri}";
        }

        if (stringEncodedUri.StartsWith("www."))
        {
            stringEncodedUri = $"https://{stringEncodedUri}";
        }


        if (Uri.TryCreate(stringEncodedUri, UriKind.RelativeOrAbsolute, out var newCreatedUri))
        {
            if (Uri.IsWellFormedUriString(stringEncodedUri, UriKind.Absolute))
            {


                CurrentSourceForDisplay = newCreatedUri.ToString();

                // TODO: WebViewService.Navigate(newCreatedUri)
                // WebViewService, corewebview -> Navigate()


                Source = newCreatedUri;
            }
        }

        IsLoading = false;
        // TODO: Show some meaningful error
    }
}
