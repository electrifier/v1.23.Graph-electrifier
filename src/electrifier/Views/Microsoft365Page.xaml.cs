using Azure.Identity;
using CommunityToolkit.Authentication;
using electrifier.Contracts.Services;
using electrifier.Helpers;
using electrifier.ViewModels;
using Microsoft.Graph;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml;
using System.Diagnostics;
using System.Net;
using Windows.System;

namespace electrifier.Views;

/// <summary>
/// A page that directly loads Microsoft 365 start page using <seealso cref="WebView2"/>.
/// Can be used on its own or navigated to within a Frame
///
/// <br/><seealso href="http://aka.ms/winui-project-info">To learn more about WinUI, the WinUI
/// project structure, and more about our project templates</seealso>
/// <br/><seealso href="https://docs.microsoft.com/microsoft-edge/webview2/">Learn more about WebView2.</seealso>
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

        ViewModel.WebViewService.Initialize(WebViewContent);
// TODO:  ViewModel.WebViewSourceUri = new Uri("Source=\"https://www.office.com/?acctsw=1&auth=1\"");
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu));
        KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.GoBack));
    }

    private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
    {
        var keyboardAccelerator = new KeyboardAccelerator() { Key = key };

        if (modifiers.HasValue)
        {
            keyboardAccelerator.Modifiers = modifiers.Value;
        }

        keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;

        return keyboardAccelerator;
    }
    private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        var navigationService = App.GetService<INavigationService>();

        Debug.Assert(navigationService != null, nameof(navigationService) + " != null");
        var result = navigationService.GoBack();

        args.Handled = result;
    }
}
