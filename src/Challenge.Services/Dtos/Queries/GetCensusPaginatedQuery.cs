using Challenge.Services.Dtos.Responses;
using MediatR;

namespace Challenge.Services.Dtos.Queries
{
    public record GetCensusPaginatedQuery(string SearchClause, short Page = 1, short ItemsPerPage = 5) : IRequest<CensusPaginatedResponseDto> { }
}
