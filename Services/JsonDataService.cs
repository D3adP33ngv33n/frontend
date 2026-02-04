using System.Net.Http.Json;
using Nostdlib.Models;

namespace Nostdlib.Services;

public class JsonDataService : IDataService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalizationService _localizationService;

    private List<ServiceItem>? _servicesCache;
    private List<JobPosition>? _jobPositionsCache;
    private List<BlogPost>? _blogPostsCache;
    private List<SocialLink>? _socialLinksCache;
    private string _cachedLanguage = "";

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
        _servicesCache = null;
        _jobPositionsCache = null;
        _blogPostsCache = null;
        _socialLinksCache = null;
        _cachedLanguage = "";
    }

    private string GetDataPath(string filename)
    {
        var lang = _localizationService.CurrentLanguage;

        // Check if we have localized data for this language
        if (lang != "en")
        {
            return $"data/{lang}/{filename}";
        }

        return $"data/{filename}";
    }

    private bool IsCacheValid()
    {
        return _cachedLanguage == _localizationService.CurrentLanguage;
    }

    public async Task<List<ServiceItem>> GetServicesAsync()
    {
        if (_servicesCache != null && IsCacheValid())
            return _servicesCache;

        try
        {
            _servicesCache = await _httpClient.GetFromJsonAsync<List<ServiceItem>>(GetDataPath("services.json"))
                ?? new List<ServiceItem>();
        }
        catch
        {
            // Fallback to English if localized file doesn't exist
            _servicesCache = await _httpClient.GetFromJsonAsync<List<ServiceItem>>("data/services.json")
                ?? new List<ServiceItem>();
        }

        _cachedLanguage = _localizationService.CurrentLanguage;
        return _servicesCache;
    }

    public async Task<List<JobPosition>> GetJobPositionsAsync()
    {
        if (_jobPositionsCache != null && IsCacheValid())
            return _jobPositionsCache;

        try
        {
            _jobPositionsCache = await _httpClient.GetFromJsonAsync<List<JobPosition>>(GetDataPath("careers.json"))
                ?? new List<JobPosition>();
        }
        catch
        {
            // Fallback to English if localized file doesn't exist
            _jobPositionsCache = await _httpClient.GetFromJsonAsync<List<JobPosition>>("data/careers.json")
                ?? new List<JobPosition>();
        }

        _cachedLanguage = _localizationService.CurrentLanguage;
        return _jobPositionsCache;
    }

    public async Task<JobPosition?> GetJobPositionByIdAsync(int id)
    {
        var positions = await GetJobPositionsAsync();
        return positions.FirstOrDefault(p => p.Id == id);
    }

    public async Task<List<BlogPost>> GetBlogPostsAsync()
    {
        if (_blogPostsCache != null && IsCacheValid())
            return _blogPostsCache;

        try
        {
            _blogPostsCache = await _httpClient.GetFromJsonAsync<List<BlogPost>>(GetDataPath("blog-posts.json"))
                ?? new List<BlogPost>();
        }
        catch
        {
            // Fallback to English if localized file doesn't exist
            _blogPostsCache = await _httpClient.GetFromJsonAsync<List<BlogPost>>("data/blog-posts.json")
                ?? new List<BlogPost>();
        }

        _cachedLanguage = _localizationService.CurrentLanguage;
        return _blogPostsCache;
    }

    public async Task<List<SocialLink>> GetSocialLinksAsync()
    {
        if (_socialLinksCache != null && IsCacheValid())
            return _socialLinksCache;

        try
        {
            _socialLinksCache = await _httpClient.GetFromJsonAsync<List<SocialLink>>(GetDataPath("social-links.json"))
                ?? new List<SocialLink>();
        }
        catch
        {
            // Fallback to English if localized file doesn't exist
            _socialLinksCache = await _httpClient.GetFromJsonAsync<List<SocialLink>>("data/social-links.json")
                ?? new List<SocialLink>();
        }

        _cachedLanguage = _localizationService.CurrentLanguage;
        return _socialLinksCache;
    }
}
