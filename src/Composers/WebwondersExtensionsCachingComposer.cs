using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Webwonders.Framework.Caching.Composers
{
    public static partial class WebwondersExtensionsCachingComposer 
    {
        public static WebApplicationBuilder RegisterCachingService(this WebApplicationBuilder builder,
            IHostEnvironment hostingEnvironment)
        {
            var services = builder.Services;

            services.AddTransient<IConfigureOptions<StaticFileOptions>, ResponseCaching>();
            services.Configure<ResponseCachingOptions>(
                builder.Configuration.GetSection("Webwonders:ResponseCaching"));
            
            return builder;
        }
    }
}
