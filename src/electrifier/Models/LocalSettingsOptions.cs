using Microsoft.UI.Xaml;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation.Metadata;

namespace electrifier.Models;

public class LocalSettingsOptions
{
    public string? ApplicationDataFolder
    {
        get; set;
    }

    public string? LocalSettingsFile
    {
        get; set;
    }

    //[WindowsRuntimeType("Microsoft.UI.Xaml")]
    //[ContractVersion(typeof(WinUIContract), 65536u)]
    public enum GuiLanguage
    {
        Default,
        English,
        German,
    }

    private LocalSettingsOptions.GuiLanguage CurrentLanguage
    {
        get
        {
            string runtimeLanguages = ResourceContext.GetForCurrentView().QualifierValues["Language"];

            // TODO: get language

            return GuiLanguage.Default;
        }

        set
        {
            string runtimeLanguages = ResourceContext.GetForCurrentView().QualifierValues["Language"];

            // TODO: set language
        }
    }
}
