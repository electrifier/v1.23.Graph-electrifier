using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using electrifier.Contracts.Services;
using Microsoft.Web.WebView2.Core;
using System.Windows.Input;

namespace electrifier.ViewModels;
public class Microsoft365ViewModel : ObservableRecipient
{
    /// <summary>Default URI to display</summary>
    private protected const string _defaultSourceOnLaunch = @"https://www.office.com/"; // https://www.office.com/?acctsw=1&auth=1

    /// <summary>Backing fields</summary>
    private string _currentSourceForDisplay = _defaultSourceOnLaunch;
    private Uri _webViewSourceUri = new(_defaultSourceOnLaunch);
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
    public Uri WebViewSourceUri
    {
        get => _webViewSourceUri;
        private set => SetProperty(ref _webViewSourceUri, value);
    }

    public Microsoft365ViewModel(IWebViewService webViewService)
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
            // TODO: Do we have to set a new URL to avoid infinite loop?
            var webErrorStatus = naviCompleted.WebErrorStatus;

            HasFailures = true;

        }
    }
    private void OnReloadCommand()
    {
        HasFailures = false;
        IsLoading = true;

        WebViewService.Reload();
    }
}
