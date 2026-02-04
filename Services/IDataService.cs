using Nostdlib.Models;

namespace Nostdlib.Services;

public interface IDataService
{
    Task<List<ServiceItem>> GetServicesAsync();
    Task<List<JobPosition>> GetJobPositionsAsync();
    Task<JobPosition?> GetJobPositionByIdAsync(int id);
    Task<List<OpenSourceProject>> GetOpenSourceProjectsAsync();
    Task<List<SocialLink>> GetSocialLinksAsync();
    void ClearCache();
}
