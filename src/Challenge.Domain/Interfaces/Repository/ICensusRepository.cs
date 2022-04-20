using Challenge.Domain.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Domain.Interfaces.Repository
{
    public interface ICensusRepository : IRepository<CensusCollection>
    {
        Task<(int totalPages, IReadOnlyCollection<CensusCollection> data)> GetCensusPaginated(short page, short itemsPerPage, string searchClause, CancellationToken cancellationToken = default);
    }
}
