using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ShopWebGisDomain.config;
using ShopWebGisElasticSearch.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebGis.HttApi.Host.Extension
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseElasticSearchRequestLogging(this IApplicationBuilder app, Action<EalsticSearchLogOption> action)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            var opts = app.ApplicationServices.GetRequiredService<IOptions<EalsticSearchLogOption>>()?.Value ?? new EalsticSearchLogOption();
            action?.Invoke(opts);
            if (string.IsNullOrWhiteSpace(opts.Url))
            {
                throw new ArgumentException($"{nameof(opts.Url)} cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(opts.LogMessagetemplate))
            {
                throw new ArgumentException($"{nameof(opts.LogMessagetemplate)} cannot be null.");
            }
            return app.UseMiddleware<ElasticSearchLoggingMiddleware>(new object[1] { opts });
        }
    }
}
