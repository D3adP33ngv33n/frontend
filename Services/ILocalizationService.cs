namespace Nostdlib.Services;

public interface ILocalizationService
{
    string CurrentLanguage { get; }
    string this[string key] { get; }
    void SetLanguage(string languageCode);
    IEnumerable<LanguageInfo> GetSupportedLanguages();
    event Action? OnLanguageChanged;
}

public record LanguageInfo(string Code, string Name, string NativeName);
