using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Challenge.IntegrationTests.Configuration
{
    public class GenericFactory<TStartUp> : WebApplicationFactory<TStartUp> where TStartUp : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseStartup<TStartUp>();
            builder.UseEnvironment("IntegrationTesting");
        }
    }
}
