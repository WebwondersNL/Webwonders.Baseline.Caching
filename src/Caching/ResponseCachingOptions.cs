namespace Webwonders.Framework.Caching;

public class ResponseCachingOptions
{
    public HashSet<string> CachedFileExtensions { get; set; } = new(StringComparer.OrdinalIgnoreCase)
    {
        ".ico", ".css", ".js", ".svg", ".woff2", ".jpg", ".webp", ".mp4"
    };
    
    public TimeSpan CacheDuration { get; set; } = TimeSpan.FromDays(366);
}