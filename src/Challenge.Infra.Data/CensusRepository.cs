using Challenge.Domain.Core.Entities;
using Challenge.Domain.Interfaces;
using Challenge.Domain.Interfaces.Repository;

namespace Challenge.Infra.Data
{
    public class CensusRepository : Repository<CensusCollection>, ICensusRepository
    {
        public CensusRepository(ICensusContext censusContext) : base(censusContext)
        {
        }
    }
}
