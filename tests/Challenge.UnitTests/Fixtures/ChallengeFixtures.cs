using Challenge.Domain.Core.Enums;
using Challenge.Services.Dtos.Commands;
using Challenge.Services.Handlers.CommandHandlers;
using Challenge.UnitTests.Faker;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Challenge.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(ChallengeFixturesCollection))]
    public class ChallengeFixturesCollection : ICollectionFixture<ChallengeFixtures> { }

    public class ChallengeFixtures : BaseFaker
    {
        public CreateCensusCommand CreateCensusCommand(int qtd)
        {
            return FakerObject<CreateCensusCommand>()
                .CustomInstantiator(c => new CreateCensusCommand(
                    FirstName: c.Person.FirstName,
                    LastName: c.Person.LastName,
                    SkinColor: c.Random.Word(),
                    Parents: new ParentsCommand(c.Person.FullName, c.Person.FullName),
                    Sons: CreateSons(qtd).ToList(),
                    Schooling: "schooling",
                    Region: c.Random.Enum<Regions>()
                    ));
        }

        public CreateCensusCommandHandler CreateCensusCommandHandler() => GenericInstance<CreateCensusCommandHandler>();

        private IEnumerable<SonCommand> CreateSons(int qtd)
        {
            var son = FakerObject<SonCommand>()
                .CustomInstantiator(s => new SonCommand(s.Person.FullName, s.Random.Int(1, 50)));

            return son.Generate(qtd);
        }
    }
}
