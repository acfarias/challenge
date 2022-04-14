using Challenge.Domain.Aggregate;
using Challenge.Domain.Builders;
using Challenge.Domain.Core.Entities;
using System.Collections.Generic;

namespace Challenge.Domain.Factories
{
    public static class CensusFactory
    {
        public static Census NewCensus(string firstName, string lastName, string skinColor, string schooling, int region, string fatherName, string motherName, List<Son> sons)
        {
            var census = new CensusBuilder(firstName, lastName, skinColor, schooling, region)
                           .AddParents(new Parents
                           {
                               FatherName = fatherName,
                               MotherName = motherName
                           })
                           .AddSons(sons);

            return census.Build();
        }
    }
}
