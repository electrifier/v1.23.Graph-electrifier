using System.Reflection;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using electrifier.Contracts.Services;
using electrifier.Helpers;

using Microsoft.UI.Xaml;

using Windows.ApplicationModel;
using Windows.Foundation.Metadata;

namespace electrifier.ViewModels;

public class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService _themeSelectorService;
    private ElementTheme _elementTheme;
    private ElementLanguage _languageId;
    private string _versionDescription;

    public ElementTheme ElementTheme
    {
        get => _elementTheme;
        set => SetProperty(ref _elementTheme, value);
    }

    public ElementLanguage LanguageId
    {
        get => _languageId; // _languageId ?? "Default"; // TODO: i18n (String "Default")
        set => SetProperty(ref _languageId, value);
    }

    public string VersionDescription
    {
        get => _versionDescription;
        set => SetProperty(ref _versionDescription, value);
    }

    public ICommand SwitchThemeCommand
    {
        get;
    }

    public ICommand SwitchLanguageCommand
    {
        get;
    }

    public SettingsViewModel(IThemeSelectorService themeSelectorService)
    {
        _themeSelectorService = themeSelectorService;
        _elementTheme = _themeSelectorService.Theme;
        // TODO: languageSelectorService
        _languageId = ElementLanguage.English;
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

        SwitchLanguageCommand = new RelayCommand<ElementLanguage>(
            async (param) =>
            {
                await _themeSelectorService.SetThemeAsync(ElementTheme.Dark);
            });
    }

    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;

            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{"AppDisplayName".GetLocalized()} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
}


//[WindowsRuntimeType("Microsoft.UI.Xaml")]
[ContractVersion(typeof(WinUIContract), 65536u)]

public enum ElementLanguage
{
    Default,
    English,
    German,
}
