using Challenge.Domain.Interfaces.Repository;
using Challenge.Services.Dtos.Queries;
using Challenge.Services.Handlers.QueryHandlers;
using Challenge.UnitTests.Constants;
using Challenge.UnitTests.Fixtures;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Challenge.UnitTests.Services.Queries
{
    [Collection(nameof(ChallengeFixturesCollection))]
    public class GetCensusPaginetedQueryHandlerTests
    {
        private readonly GetCensusPaginatedQueryHandler _getCensusPaginatedQueryHandler;
        private readonly ChallengeFixtures _challengeFixtures;

        public GetCensusPaginetedQueryHandlerTests(ChallengeFixtures challengeFixtures)
        {
            _getCensusPaginatedQueryHandler = challengeFixtures.GetCensusPaginatedQueryHandler();
            _challengeFixtures = challengeFixtures;
        }

        [Trait(TraitsConstants.TraitService, nameof(GetCensusPaginetedQueryHandlerTests))]
        [Fact(DisplayName = "Get Census Paginated - Success")]
        public async Task GetCensusPaginated_Success()
        {
            // Arrange
            var censusCollection = _challengeFixtures.GetCensusCollections(8);
            _challengeFixtures.AutoMocker.GetMock<ICensusRepository>().Setup(c => c.GetCensusPaginated(It.IsAny<short>(), It.IsAny<short>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync((2, censusCollection));

            // Act
            var request = await _getCensusPaginatedQueryHandler.Handle(new GetCensusPaginatedQuery(string.Empty, 1, 5), new CancellationToken());

            // Asserts
            Assert.Equal(2, request.TotalPages);
            Assert.True(request.Census.Count() == 8);
        }

        [Trait(TraitsConstants.TraitService, nameof(GetCensusPaginetedQueryHandlerTests))]
        [Fact(DisplayName = "Get Census Paginated - Token Canceled - Fail")]
        public async Task GetCensusPaginated_TokenCanceled_Fail()
        {
            // Arrange & Act
            var request = await _getCensusPaginatedQueryHandler.Handle(new GetCensusPaginatedQuery(string.Empty, 1, 5), new CancellationToken(true));

            // Asserts
            Assert.Null(request);
        }
    }
}
