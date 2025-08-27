using Challenge.Domain.Core.Entities;
using Challenge.Domain.Core.Enums;
using Challenge.Services.Dtos.Commands;
using Challenge.Services.Dtos.Queries;
using Challenge.Services.Handlers.CommandHandlers;
using Challenge.Services.Handlers.QueryHandlers;
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

        private IEnumerable<SonCommand> CreateSons(int qtd)
        {
            var son = FakerObject<SonCommand>()
                .CustomInstantiator(s => new SonCommand(s.Person.FullName, s.Random.Int(1, 50)));

            return son.Generate(qtd);
        }

        public IReadOnlyCollection<CensusCollection> GetCensusCollections(int qtd)
        {
            var census = FakerObject<CensusCollection>()
                .CustomInstantiator(c => new CensusCollection
                {
                    Id = c.Random.Guid().ToString(),
                    FirstName = c.Person.FirstName,
                    LastName = c.Person.LastName,
                    Parents = new Parents
                    {
                        FatherName = c.Person.FullName,
                        MotherName = c.Person.FullName
                    },
                    Region = (int)c.Random.Enum<Regions>(),
                    Schooling = c.Random.Word(),
                    SkinColor = c.Random.Word(),
                    Sons = new List<Son>
                    {
                        new Son
                        {
                            Age = c.Random.Int(1,80),
                            FullName = c.Person.FullName
                        },
                        new Son
                        {
                            Age = c.Random.Int(1,80),
                            FullName = c.Person.FullName
                        }
                    }
                });

            return census.Generate(qtd);
        }

        public CreateCensusCommandHandler CreateCensusCommandHandler() => GenericInstance<CreateCensusCommandHandler>();
        public GetCensusPaginatedQueryHandler GetCensusPaginatedQueryHandler() => GenericInstance<GetCensusPaginatedQueryHandler>();
    }
}