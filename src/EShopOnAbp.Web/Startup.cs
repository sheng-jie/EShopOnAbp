using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EShopOnAbp.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<EShopOnAbpWebModule>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();
        }
    }
}
