using System.Collections.Generic;

namespace Challenge.Services.Dtos.Responses
{
    public record CensusPaginatedResponseDto(IEnumerable<CensusDto> Census, int TotalPages) { }
}
