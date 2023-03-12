using electrifier.Models;
using electrifier.Models.Configuration.Global;

namespace electrifier.Contracts.Services;

public interface ILocalSettingsService
{
    Task DeleteSettingAsync<T>(string key);

    Task<T> GetSettingAsync<T>(string key, T defaultValue);

    Task<T?> ReadSettingAsync<T>(string key);

    Task SaveSettingAsync<T>(string key, T value);

    Task SetGuiLanguageAsync(LocalSettingsOptions.GuiLanguage language);
}