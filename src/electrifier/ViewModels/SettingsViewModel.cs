using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using electrifier.Contracts.Services;
using electrifier.Helpers;
using Microsoft.UI.Xaml;
using System.Windows.Input;
using Windows.ApplicationModel;
using electrifier.Models.Configuration.Global;

namespace electrifier.ViewModels;

public class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService _themeSelectorService;
    private ElementTheme _elementTheme;
    private readonly ILocalSettingsService _localSettingsService;
    private LocalSettingsOptions.GuiLanguage _guiLanguage = LocalSettingsOptions.GuiLanguage.Default;
    private string _versionDescription;

#if DEBUG
    public static bool DebugDescriptor => true;
#else
    public static bool DebugDescriptor => false;
#endif

    public ElementTheme ElementTheme
    {
        get => _elementTheme;
        set => SetProperty(ref _elementTheme, value);
    }

    public LocalSettingsOptions.GuiLanguage GuiLanguage
    {
        get => _guiLanguage; // _languageId ?? "Default"; // TODO: i18n (String "Default")
        set => SetProperty(ref _guiLanguage, value);
    }

    public string VersionDescription
    {
        get => _versionDescription;
        private set => SetProperty(ref _versionDescription, value);
    }

    public ICommand SwitchThemeCommand
    {
        get;
    }

    public ICommand SwitchLanguageCommand
    {
        get;
    }

    public SettingsViewModel(ILocalSettingsService localSettingsService, IThemeSelectorService themeSelectorService)
    {
        _localSettingsService = localSettingsService ?? throw new ArgumentNullException(nameof(localSettingsService));
        _themeSelectorService = themeSelectorService ?? throw new ArgumentNullException(nameof(themeSelectorService));

        _elementTheme = _themeSelectorService.Theme;
        _guiLanguage = LocalSettingsOptions.GuiLanguage.Default;
        _versionDescription = GetVersionDescription();

        SwitchThemeCommand = new RelayCommand<ElementTheme>(
            async (param) =>
            {
                if (ElementTheme != param)
                {
                    ElementTheme = param;
                    await _themeSelectorService.SetThemeAsync(param);
                }
            });

        SwitchLanguageCommand = new RelayCommand<LocalSettingsOptions.GuiLanguage>(
            async (param) =>
            {
                if (_guiLanguage != param)
                {
                    _guiLanguage = param;
                    await _localSettingsService.SetGuiLanguageAsync(param);
                }
            });
    }

    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;

            version = new Version(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            //            version = Assembly.GetExecutingAssembly().GetName().Version;
            version = new Version(1, 0);
        }

        var versionDescription = $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";

        return versionDescription;
    }
}