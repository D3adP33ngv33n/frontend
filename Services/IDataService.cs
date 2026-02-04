using Nostdlib.Models;

namespace Nostdlib.Services;

public interface IDataService
{
    Task<List<ServiceItem>> GetServicesAsync();
    Task<List<JobPosition>> GetJobPositionsAsync();
    Task<JobPosition?> GetJobPositionByIdAsync(int id);
    Task<List<BlogPost>> GetBlogPostsAsync();
    Task<List<SocialLink>> GetSocialLinksAsync();
}
