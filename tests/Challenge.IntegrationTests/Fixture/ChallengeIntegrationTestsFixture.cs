using Challenge.Api;
using Challenge.IntegrationTests.Configuration;
using Challenge.UnitTests.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Challenge.IntegrationTests.Fixture
{
    [CollectionDefinition(nameof(ChallengeIntegrationTestsFixtureCollection))]
    public class ChallengeIntegrationTestsFixtureCollection : ICollectionFixture<ChallengeIntegrationTestsFixture>,
                                                              ICollectionFixture<ChallengeFixtures> { }

    public class ChallengeIntegrationTestsFixture : IDisposable
    {
        public readonly GenericFactory<StartupIntegrationTests> Factory;

        public HttpClient Client;

        public ChallengeIntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost:5011")
            };

            Factory = new GenericFactory<StartupIntegrationTests>();
            Client = Factory.CreateClient(clientOptions);
        }

        public void Dispose()
        {
            Factory.Dispose();
            Client.Dispose();
        }
    }
}
