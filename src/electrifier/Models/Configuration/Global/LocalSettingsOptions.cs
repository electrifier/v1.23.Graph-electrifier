using Windows.ApplicationModel.Resources.Core;

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
            var runtimeLanguages = ResourceContext.GetForCurrentView().QualifierValues["Language"];

            // TODO: get language

            return GuiLanguage.Default;
        }

        set
        {
            var runtimeLanguages = ResourceContext.GetForCurrentView().QualifierValues["Language"];

            //if (runtimeLanguages.Contains(value))
            //{
            //    // TODO: set language
            //}
            //                throw new InvalidOperationException("");
        }
    }
}