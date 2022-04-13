using Challenge.Domain.Aggregate;
using Challenge.Domain.Builders;
using Challenge.Domain.Core.Entities;
using System.Collections.Generic;

namespace Challenge.Domain.Factories
{
    public static class CensusFactory
    {
        public static Census NewCensus(string firstName, string lastName, string skinColor, string schooling, int region, Parents parent, List<Son> sons)
        {
            var census = new CensusBuilder(firstName, lastName, skinColor, schooling, region)
                           .AddParents(parent)
                           .AddSons(sons);

            return census.Build();
        }
    }
}
