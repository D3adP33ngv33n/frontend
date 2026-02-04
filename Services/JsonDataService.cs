using System.Net.Http.Json;
using Nostdlib.Models;

namespace Nostdlib.Services;

public class JsonDataService : IDataService
{
    private readonly HttpClient _httpClient;

    private List<ServiceItem>? _servicesCache;
    private List<JobPosition>? _jobPositionsCache;
    private List<BlogPost>? _blogPostsCache;
    private List<SocialLink>? _socialLinksCache;

    public JsonDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ServiceItem>> GetServicesAsync()
    {
        _servicesCache ??= await _httpClient.GetFromJsonAsync<List<ServiceItem>>("data/services.json")
            ?? new List<ServiceItem>();
        return _servicesCache;
    }

    public async Task<List<JobPosition>> GetJobPositionsAsync()
    {
        _jobPositionsCache ??= await _httpClient.GetFromJsonAsync<List<JobPosition>>("data/careers.json")
            ?? new List<JobPosition>();
        return _jobPositionsCache;
    }

    public async Task<JobPosition?> GetJobPositionByIdAsync(int id)
    {
        var positions = await GetJobPositionsAsync();
        return positions.FirstOrDefault(p => p.Id == id);
    }

    public async Task<List<BlogPost>> GetBlogPostsAsync()
    {
        _blogPostsCache ??= await _httpClient.GetFromJsonAsync<List<BlogPost>>("data/blog-posts.json")
            ?? new List<BlogPost>();
        return _blogPostsCache;
    }

    public async Task<List<SocialLink>> GetSocialLinksAsync()
    {
        _socialLinksCache ??= await _httpClient.GetFromJsonAsync<List<SocialLink>>("data/social-links.json")
            ?? new List<SocialLink>();
        return _socialLinksCache;
    }
}
