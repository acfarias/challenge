using System.Net.Http;
using System.Net.Http.Headers;

namespace Challenge.IntegrationTests.Configuration
{
    public static class TestsExtensions
    {
        private static void SetJsonMediaType(this HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}