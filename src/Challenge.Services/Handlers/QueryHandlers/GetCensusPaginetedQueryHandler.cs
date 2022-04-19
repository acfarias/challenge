using Challenge.Domain.Core.Enums;
using Challenge.Domain.Interfaces.Repository;
using Challenge.Services.Dtos;
using Challenge.Services.Dtos.Queries;
using Challenge.Services.Dtos.Responses;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Services.Handlers.QueryHandlers
{
    public class GetCensusPaginetedQueryHandler : IRequestHandler<GetCensusPaginatedQuery, CensusPaginatedResponseDto>
    {
        private readonly ICensusRepository _censusRepository;

        public GetCensusPaginetedQueryHandler(ICensusRepository censusRepository)
        {
            _censusRepository = censusRepository;
        }

        public async Task<CensusPaginatedResponseDto> Handle(GetCensusPaginatedQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return null;

            var dataResult = await _censusRepository.GetPaginated(request.Page, request.ItemsPerPage, request.SearchClause, cancellationToken);

            return new CensusPaginatedResponseDto(dataResult.data.Select(c => new CensusDto(c.FirstName, c.LastName, c.SkinColor, new ParentsDto(c.Parents.FatherName, c.Parents.MotherName), c.Sons.Select(s => new SonDto(s.FullName, s.Age)).ToList(), c.Schooling, (Regions)c.Region)), dataResult.totalPages);
        }
    }
}
