namespace Nostdlib.Constants;

public static class Routes
{
    public const string Home = "/";
    public const string Blog = "/blog";
    public const string BlogEc = "/blog/ec";
    public const string BlogCppPic = "/blog/cpp-pic";
    public const string BlogNoRwx = "/blog/norwx";
    public const string Careers = "/#careers";
    public const string Privacy = "/privacy";
    public const string Security = "/security";

    public static string GetCareerDetailsUrl(int id) => $"/careers/{id}";
}
