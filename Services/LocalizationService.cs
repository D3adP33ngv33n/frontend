using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace Nostdlib.Services;

public class LocalizationService : ILocalizationService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly HttpClient _httpClient;
    private Dictionary<string, string> _currentResources = new();
    private Dictionary<string, string>? _fallbackResources;
    private string _currentLanguage = "en";
    private bool _isInitialized;

    public event Action? OnLanguageChanged;

    private static readonly IReadOnlyList<LanguageInfo> SupportedLanguagesList = new List<LanguageInfo>
    {
        new("en", "English", "English"),
        new("ru", "Russian", "\u0420\u0443\u0441\u0441\u043a\u0438\u0439"),
        new("hy", "Armenian", "\u0540\u0561\u0575\u0565\u0580\u0565\u0576")
    };

    private static readonly HashSet<string> SupportedLanguageCodes = new(
        SupportedLanguagesList.Select(l => l.Code),
        StringComparer.OrdinalIgnoreCase
    );

    public LocalizationService(IJSRuntime jsRuntime, HttpClient httpClient)
    {
        _jsRuntime = jsRuntime;
        _httpClient = httpClient;
    }

    public string CurrentLanguage => _currentLanguage;

    public string this[string key]
    {
        get
        {
            if (_currentResources.TryGetValue(key, out var value))
            {
                return value;
            }

            // Fallback to English
            if (_fallbackResources?.TryGetValue(key, out var enValue) == true)
            {
                return enValue;
            }

            return key;
        }
    }

    public async Task SetLanguageAsync(string languageCode)
    {
        if (!SupportedLanguageCodes.Contains(languageCode) || _currentLanguage == languageCode)
            return;

        _currentLanguage = languageCode;
        await LoadResourcesAsync(languageCode);

        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "language", languageCode);
        }
        catch
        {
            // localStorage might not be available during prerendering
        }

        OnLanguageChanged?.Invoke();
    }

    public async Task InitializeAsync()
    {
        if (_isInitialized) return;

        try
        {
            // Load English as fallback first
            _fallbackResources = await LoadLocaleFileAsync("en");

            // Try to get saved language preference
            var savedLanguage = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "language");
            if (!string.IsNullOrEmpty(savedLanguage) && SupportedLanguageCodes.Contains(savedLanguage))
            {
                _currentLanguage = savedLanguage;
            }

            // Load current language resources
            await LoadResourcesAsync(_currentLanguage);
            _isInitialized = true;
        }
        catch
        {
            // localStorage might not be available, use defaults
            _currentResources = _fallbackResources ?? new Dictionary<string, string>();
            _isInitialized = true;
        }
    }

    public IReadOnlyList<LanguageInfo> GetSupportedLanguages() => SupportedLanguagesList;

    private async Task LoadResourcesAsync(string languageCode)
    {
        if (languageCode == "en" && _fallbackResources != null)
        {
            _currentResources = _fallbackResources;
            return;
        }

        var resources = await LoadLocaleFileAsync(languageCode);
        _currentResources = resources ?? _fallbackResources ?? new Dictionary<string, string>();
    }

    private async Task<Dictionary<string, string>?> LoadLocaleFileAsync(string languageCode)
    {
        var path = $"data/{languageCode}/locale.json";

        try
        {
            return await _httpClient.GetFromJsonAsync<Dictionary<string, string>>(path);
        }
        catch
        {
            // If file doesn't exist or fails to load, return null to use fallback
            return null;
        }
    }
}
