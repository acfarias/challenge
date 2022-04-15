using Challenge.IntegrationTests.Configuration;
using Challenge.IntegrationTests.Fixture;
using Challenge.UnitTests.Constants;
using Challenge.UnitTests.Fixtures;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Challenge.IntegrationTests.ChallengeControllerTest
{
    [TestCaseOrderer(ConstantsConfig.OrderType, ConstantsConfig.OrderAssembly)]
    [Collection(nameof(ChallengeIntegrationTestsFixtureCollection))]
    public class ChallengeIntegrationTests
    {
        private readonly ChallengeIntegrationTestsFixture _challengeIntegrationTestsFixture;
        private readonly ChallengeFixtures _challengeFixtures;

        public ChallengeIntegrationTests(ChallengeIntegrationTestsFixture challengeIntegrationTestsFixture, ChallengeFixtures challengeFixtures)
        {
            _challengeIntegrationTestsFixture = challengeIntegrationTestsFixture;
            _challengeFixtures = challengeFixtures;
        }

        [Trait(TraitsConstants.TraitPresentation, nameof(ChallengeIntegrationTests))]
        [Fact(DisplayName = "Create Census - Success"), OrderTest(1)]
        public async Task CreateCensus_Success()
        {
            // Arrange
            var censusCommand = _challengeFixtures.CreateCensusCommand(2);

            // Act
            var request = await _challengeIntegrationTestsFixture.Client.PostAsJsonAsync("census", censusCommand);

            // Asserts
            request.EnsureSuccessStatusCode();
            var objectResult = JsonConvert.DeserializeObject<bool>(await request.Content.ReadAsStringAsync());
            Assert.True(objectResult);
        }
    }
}
