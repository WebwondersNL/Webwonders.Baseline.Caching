using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Umbraco.Cms.Core.Hosting;


namespace Webwonders.Framework.Caching;

public class ResponseCaching : IConfigureOptions<StaticFileOptions>
{
    private readonly ResponseCachingOptions _options;
    private readonly string _backOfficePath;

    public ResponseCaching(
        IHostingEnvironment hostingEnvironment,
        IOptions<ResponseCachingOptions> options)
    {
        //_backOfficePath = hostingEnvironment.GetBackOfficePath(); For Version 16.
        _backOfficePath = "/umbraco";
        _options = options.Value;
    }


    public void Configure(StaticFileOptions options)
        => options.OnPrepareResponse = ctx =>
        {
            if (ctx.Context.Request.Path.StartsWithSegments(_backOfficePath))
            {
                return;
            }

            var fileExtension = Path.GetExtension(ctx.File.Name);
            if (_options.CachedFileExtensions.Contains(fileExtension))
            {
                ResponseHeaders headers = ctx.Context.Response.GetTypedHeaders();
                CacheControlHeaderValue cacheControl = headers.CacheControl ?? new CacheControlHeaderValue();
                cacheControl.Public = true;
                cacheControl.MaxAge = _options.CacheDuration;
                headers.CacheControl = cacheControl;

            }
        };

}