using Challenge.Domain.Core.Enums;
using System.Collections.Generic;

namespace Challenge.Services.Dtos
{
    public record CensusDto(string FirstName, string LastName, string SkinColor, ParentsDto Parents, List<SonDto> Sons, string Schooling, Regions Region) { }

    public record ParentsDto(string FatherName, string MotherName) { }
    public record SonDto(string Name, int Age) { }
}
