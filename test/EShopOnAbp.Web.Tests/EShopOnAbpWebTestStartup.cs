using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace EShopOnAbp
{
    public class EShopOnAbpWebTestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<EShopOnAbpWebTestModule>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
}