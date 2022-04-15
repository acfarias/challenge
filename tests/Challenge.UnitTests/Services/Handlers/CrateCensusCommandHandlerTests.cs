using Challenge.Domain.Core.Entities;
using Challenge.Domain.Interfaces.Repository;
using Challenge.Services.Handlers.CommandHandlers;
using Challenge.UnitTests.Constants;
using Challenge.UnitTests.Fixtures;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Challenge.UnitTests.Services.Handlers
{
    [Collection(nameof(ChallengeFixturesCollection))]
    public class CrateCensusCommandHandlerTests
    {
        private readonly ChallengeFixtures _challengeFixtures;
        private readonly CreateCensusCommandHandler _createCensusCommandHandler;

        public CrateCensusCommandHandlerTests(ChallengeFixtures challengeFixtures)
        {
            _challengeFixtures = challengeFixtures;
            _createCensusCommandHandler = challengeFixtures.CreateCensusCommandHandler();
        }

        [Trait(TraitsConstants.TraitService, nameof(CrateCensusCommandHandlerTests))]
        [Fact(DisplayName = "Create Census - Success")]
        public async Task CreateCensus_Success()
        {
            // Arrange
            var command = _challengeFixtures.CreateCensusCommand(2);
            _challengeFixtures.AutoMocker.GetMock<IMediator>().Setup(m => m.Send(It.IsAny<CreateCensusCommandHandler>(), It.IsAny<CancellationToken>()));
            _challengeFixtures.AutoMocker.GetMock<ICensusRepository>().Setup(c => c.Create(It.IsAny<CensusCollection>(), It.IsAny<CancellationToken>())).ReturnsAsync(new CensusCollection { FirstName = command.FirstName });

            // Act
            var request = await _createCensusCommandHandler.Handle(command, new CancellationToken());

            // Asserts
            Assert.True(request);
        }

        [Trait(TraitsConstants.TraitService, nameof(CrateCensusCommandHandlerTests))]
        [Fact(DisplayName = "Create Census - Token Canceled - Fail")]
        public async Task CreateCensus_TokenCanceled_Fail()
        {
            // Arrange
            var command = _challengeFixtures.CreateCensusCommand(2);

            // Act
            var request = await _createCensusCommandHandler.Handle(command, new CancellationToken(true));

            // Asserts
            Assert.False(request);
        }
    }
}