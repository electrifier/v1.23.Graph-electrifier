// Disable XAML Generated break on unhalted exception
// <seealso href="" />
//#define DISABLE_XAML_GENERATED_BREAK_ON_UNHANDLED_EXCEPTION


using System.Diagnostics;
using System.Text;
using CommunityToolkit.WinUI;
using electrifier.Activation;
using electrifier.Contracts.Services;
using electrifier.Models;
using electrifier.Models.Configuration.Global;
using electrifier.Services;
using electrifier.ViewModels;
using electrifier.Views;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

namespace electrifier;

// To learn more about WinUI 3, see https://docs.microsoft.com/windows/apps/winui/winui3/.
public partial class App : Application
{
    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    public IHost Host
    {
        get;
    }

    public static T? GetService<T>()
        where T : class
    {
        try
        {
            if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
            {
                throw new ArgumentException(
                    $"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
            }

            return service;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);
        }

        return null;
    }

    public static WindowEx MainWindow { get; } = new MainWindow();

    public App()
    {
        InitializeComponent();

        //UnhandledException += App_UnhandledException;

        Host = Microsoft.Extensions.Hosting.Host.
            CreateDefaultBuilder().
            UseContentRoot(AppContext.BaseDirectory).
            ConfigureServices((context, services) =>
            {
                // Default Activation Handler
                _ = services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

                // Other Activation Handlers
                services.AddTransient<IActivationHandler, AppNotificationActivationHandler>();

                // Services
                services.AddSingleton<IAppNotificationService, AppNotificationService>();
                services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
                services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
                services.AddTransient<INavigationViewService, NavigationViewService>();
                services.AddTransient<IWebViewService, WebViewService>();

                services.AddSingleton<IActivationService, ActivationService>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<IPageService, PageService>();

                // Core Services
                services.AddSingleton<IFileService, FileService>();

                // Views and ViewModels
                services.AddTransient<ClipboardPage>();
                services.AddTransient<ClipboardViewModel>();
                services.AddTransient<DevicesPage>();
                services.AddTransient<DevicesViewModel>();
                services.AddTransient<FileManagerPage>();
                services.AddTransient<FileManagerViewModel>();
                services.AddTransient<NetworkDevicesPage>();
                services.AddTransient<NetworkDevicesViewModel>();
                services.AddTransient<SettingsPage>();
                services.AddTransient<SettingsViewModel>();
                services.AddTransient<ShellPage>();
                services.AddTransient<ShellViewModel>();
                services.AddTransient<WebFavoritesPage>();
                services.AddTransient<WebFavoritesViewModel>();
                services.AddTransient<WebHostsPage>();
                services.AddTransient<WebHostsViewModel>();
                services.AddTransient<WebViewPage>();
                services.AddTransient<WebViewViewModel>();
                services.AddTransient<WorkbenchPage>();
                services.AddTransient<WorkbenchViewModel>();

                // Configuration
                services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
            }).Build();

        GetService<IAppNotificationService>()?
            .Initialize();

        UnhandledException += App_UnhandledException;
    }

    //private void App_StartAppCenter()
    //{
    //    AppCenter.Start("{ TODO:Your_app_secret_here }", typeof(Analytics), typeof(Crashes));
    //}


    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs args)
    {
        App_UnhandledException(sender, args, false);
    }

    /// <summary>
    /// Log and handle exceptions as appropriate.
    /// 
    /// <seealso href="https://docs.microsoft.com/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.unhandledexception."/>
    /// 
    /// <seealso href="https://learn.microsoft.com/en-us/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.unhandledexception?view=windows-app-sdk-1.2"/>
    /// 
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    /// <param name="itIsComplicated">
    ///     Set <b>true</b> to force shutdown in case of critical error.<br/>
    ///     <br/>
    ///     Triggers <see cref="Microsoft.UI.Xaml.UnhandledExceptionEventArgs.Handled"/>.
    /// </param>
    private void App_UnhandledException(
        object sender,
        Microsoft.UI.Xaml.UnhandledExceptionEventArgs args,
        bool itIsComplicated)
    {
        try
        {
            StringBuilder sb = new();

            sb.AppendJoin("\n", "Exception happened!", "line 1", "line 2");

            // TODO: Try to make an backup of current configuration and mark as "dirty".
            // TODO: Log and handle exceptions as appropriate.

            //guru?.ThrowGuruMeditation(sender, args);

            /*

            private async void ShowDialog_Click(object sender, RoutedEventArgs e)
            {
                ContentDialog dialog = new ContentDialog();

                // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
                dialog.XamlRoot = this.XamlRoot;
                dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
                dialog.Title = "Save your work?";
                dialog.PrimaryButtonText = "Save";
                dialog.SecondaryButtonText = "Don't Save";
                dialog.CloseButtonText = "Cancel";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = new ContentDialogContent();

                var result = await dialog.ShowAsync();
            }

            */


            //     if (args is not null)
            //     {
            //         // TODO: Exception happened!
            //     
            //         args.Handled = true;
            //     }
            //     else
            //     {
            //         throw new ArgumentNullException(nameof(args));
            //     }
            //     if (!itIsComplicated)
            //     {
            //         args?.Handled = true;
            //     }
            // }
        }
        catch (Exception ex)
        {
            var dummy = ex.ToString();


            // TODO: Log inner exception
        }
        finally
        {
                args.Handled = true;                // TODO: For test purposes only
                //args.Handled = !itIsComplicated; // TODO 
        }
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        //GetService<IAppNotificationService>()?
        //    .Show(string.Format("AppNotificationSamplePayload".GetLocalized(), AppContext.BaseDirectory));

        await GetService<IActivationService>()?.ActivateAsync(args);
    }
}