using CommunityToolkit.Mvvm.ComponentModel;
using electrifier.Contracts.Services;
using electrifier.ViewModels;
using electrifier.Views;
using Microsoft.UI.Xaml.Controls;

namespace electrifier.Services;

public class PageService : IPageService
{
    private readonly Dictionary<string, Type> _pages = new();

    public PageService()
    {
        Configure<ClipboardViewModel, ClipboardPage>();
        Configure<DevicesViewModel, DevicesPage>();
        Configure<FileManagerViewModel, FileManagerPage>();
        Configure<Microsoft365ViewModel, Microsoft365Page>();
        Configure<NetworkDevicesViewModel, NetworkDevicesPage>();
        Configure<SettingsViewModel, SettingsPage>();
        Configure<WebFavoritesViewModel, WebFavoritesPage>();
        Configure<WebHostsViewModel, WebHostsPage>();
        Configure<WebViewViewModel, WebViewPage>();
        Configure<WorkbenchViewModel, WorkbenchPage>();
    }

    public Type GetPageType(string key)
    {
        Type? pageType;
        lock (_pages)
        {
            if (!_pages.TryGetValue(key, out pageType))
            {
                throw new ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?");
            }
        }

        return pageType;
    }

    private void Configure<TObservableObjectViewModel, TPage>()
        where TObservableObjectViewModel : ObservableObject
        where TPage : Page
    {
        lock (_pages)
        {
            var key = typeof(TObservableObjectViewModel).FullName!;
            if (_pages.ContainsKey(key))
            {
                throw new ArgumentException($"The key {key} is already configured in PageService");
            }

            var type = typeof(TPage);
            if (_pages.Any(p => p.Value == type))
            {
                throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == type).Key}");
            }

            _pages.Add(key, type);
        }
    }
}