namespace Nostdlib.Models;

public record BlogPost(
    string Title,
    string Purpose,
    string Description,
    List<string> Features,
    List<string> UseCases,
    string Technologies,
    string Status,
    string StatusClass,
    string Audience,
    string License,
    string Icon,
    string? GitHubUrl = null
);
