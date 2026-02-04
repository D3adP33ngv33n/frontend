namespace Nostdlib.Models;

public record BlogPost(
    string Title,
    string Purpose,
    List<string> Features,
    string Technologies,
    string Status,
    string Audience,
    string Url,
    string Icon,
    string? GitHubUrl = null
);
