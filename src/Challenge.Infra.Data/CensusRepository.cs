using Challenge.Domain.Core.Entities;
using Challenge.Domain.Interfaces;
using Challenge.Domain.Interfaces.Repository;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson;
using System.Text.RegularExpressions;

namespace Challenge.Infra.Data
{
    public class CensusRepository : Repository<CensusCollection>, ICensusRepository
    {
        private readonly IMongoCollection<CensusCollection> _entityCollection;

        public CensusRepository(ICensusContext censusContext) : base(censusContext)
        {
            _entityCollection = censusContext._dataBase.GetCollection<CensusCollection>(typeof(CensusCollection).Name);
        }

        public async Task<(int totalPages, IReadOnlyCollection<CensusCollection> data)> GetPaginated(short page, short itemsPerPage, string searchClause, CancellationToken cancellationToken = default)
        {
            var countFacet = AggregateFacet.Create("count", PipelineDefinition<CensusCollection, AggregateCountResult>.Create(new[]
            {
                PipelineStageDefinitionBuilder.Count<CensusCollection>()
            }));

            var dataFacet = AggregateFacet.Create("data",
            PipelineDefinition<CensusCollection, CensusCollection>.Create(new[]
            {
                PipelineStageDefinitionBuilder.Sort(Builders<CensusCollection>.Sort.Ascending(x => x.FirstName)),
                PipelineStageDefinitionBuilder.Skip<CensusCollection>((page - 1) * itemsPerPage),
                PipelineStageDefinitionBuilder.Limit<CensusCollection>(itemsPerPage),
            }));

            var filter = Builders<CensusCollection>.Filter.Empty;
            if (!string.IsNullOrWhiteSpace(searchClause))
                filter = Builders<CensusCollection>.Filter.Regex(c => c.FirstName, BsonRegularExpression.Create(Regex.Escape(searchClause)));

            var aggregation = await _entityCollection.Aggregate()
                                    .Match(filter)
                                    .Facet(countFacet, dataFacet)
                                    .ToListAsync(cancellationToken);

            var count = aggregation.First()
                                 .Facets.First(f => f.Name == "count")
                                 .Output<AggregateCountResult>()?
                                 .FirstOrDefault()?.Count ?? 0;

            var totalPages = (int)count / itemsPerPage;

            var data = aggregation.First()
                                .Facets.First(f => f.Name == "data")
                                .Output<CensusCollection>();

            return (totalPages, data);
        }
    }
}