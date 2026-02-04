using System.Net.Http.Json;
using Nostdlib.Models;

namespace Nostdlib.Services;

public class JsonDataService : IDataService, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly ILocalizationService _localizationService;
    private readonly Dictionary<string, object> _cache = new();
    private string _cachedLanguage = "";
    private bool _disposed;

    public JsonDataService(HttpClient httpClient, ILocalizationService localizationService)
    {
        _httpClient = httpClient;
        _localizationService = localizationService;
        _localizationService.OnLanguageChanged += OnLanguageChanged;
    }

    private void OnLanguageChanged()
    {
        ClearCache();
    }

    public void ClearCache()
    {
        _cache.Clear();
        _cachedLanguage = "";
    }

    private string GetDataPath(string filename)
    {
        var lang = _localizationService.CurrentLanguage;
        return $"data/{lang}/{filename}";
    }

    private bool IsCacheValid() => _cachedLanguage == _localizationService.CurrentLanguage;

    private async Task<List<T>> LoadDataAsync<T>(string filename, string cacheKey)
    {
        if (_cache.TryGetValue(cacheKey, out var cached) && cached is List<T> cachedList && IsCacheValid())
            return cachedList;

        List<T>? result = null;

        try
        {
            result = await _httpClient.GetFromJsonAsync<List<T>>(GetDataPath(filename));
        }
        catch
        {
            // Fallback to English if localized file doesn't exist
            try
            {
                result = await _httpClient.GetFromJsonAsync<List<T>>($"data/en/{filename}");
            }
            catch
            {
                // If both fail, return empty list
            }
        }

        result ??= new List<T>();
        _cache[cacheKey] = result;
        _cachedLanguage = _localizationService.CurrentLanguage;

        return result;
    }

    public Task<List<ServiceItem>> GetServicesAsync()
        => LoadDataAsync<ServiceItem>("services.json", nameof(ServiceItem));

    public Task<List<JobPosition>> GetJobPositionsAsync()
        => LoadDataAsync<JobPosition>("careers.json", nameof(JobPosition));

    public async Task<JobPosition?> GetJobPositionByIdAsync(int id)
    {
        var positions = await GetJobPositionsAsync();
        return positions.FirstOrDefault(p => p.Id == id);
    }

    public Task<List<OpenSourceProject>> GetOpenSourceProjectsAsync()
        => LoadDataAsync<OpenSourceProject>("opensource-projects.json", nameof(OpenSourceProject));

    public Task<List<SocialLink>> GetSocialLinksAsync()
        => LoadDataAsync<SocialLink>("social-links.json", nameof(SocialLink));

    public void Dispose()
    {
        if (_disposed)
            return;

        _localizationService.OnLanguageChanged -= OnLanguageChanged;
        _cache.Clear();
        _disposed = true;

        GC.SuppressFinalize(this);
    }
}
