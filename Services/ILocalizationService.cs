namespace Nostdlib.Services;

public interface ILocalizationService
{
    string CurrentLanguage { get; }
    string this[string key] { get; }
    Task SetLanguageAsync(string languageCode);
    Task InitializeAsync();
    IReadOnlyList<LanguageInfo> GetSupportedLanguages();
    event Action? OnLanguageChanged;
}

public record LanguageInfo(string Code, string Name, string NativeName);
