namespace Nostdlib.Models;

public record JobPosition(
    int Id,
    string Code,
    string Title,
    string Level,
    string Location,
    string Type,
    string? Description = null,
    List<string>? Requirements = null
);
