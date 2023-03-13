using Microsoft.UI.Xaml.Controls;

namespace electrifier.Contracts.Services;

public interface INavigationViewService
{
    IList<object>? MenuItems
    {
        get;
    }

    object? SettingsItem
    {
        get;
    }

    void Initialize(NavigationView navigationView);

    void UnregisterMyEvents();

    NavigationViewItem? GetSelectedItem(Type pageType);
}